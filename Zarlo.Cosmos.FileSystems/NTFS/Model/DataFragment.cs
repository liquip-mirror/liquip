﻿namespace Zarlo.Cosmos.FileSystems.NTFS.Model;

public class DataFragment
{
    /// <summary>
    ///     Note. This is not the size in bytes of this fragment. It is a very compact NTFS-specific way of writing lengths.
    ///     Be warned when using it.
    /// </summary>
    public byte FragmentSizeBytes { get; set; }

    public long Clusters { get; set; }
    public byte CompressedClusters { get; set; }
    public long LCN { get; set; }
    public long StartingVCN { get; set; }
    public int ThisObjectLength { get; private set; }

    /// <summary>
    ///     If this fragment is sparse, there is not data on the disk to reflect the data in the file.
    ///     This chunk is therefore all zeroes.
    /// </summary>
    public bool IsSparseFragment => LCN == 0;

    /// <summary>
    ///     If this fragment is compressed, it will contain some clusters on disk (The Clusters property) which contain actual
    ///     (compressed) data.
    ///     After that, there is a number of clusters (CompressedClusters property) which act as 'fillers' for the decompressed
    ///     data. It does not exist on disk.
    /// </summary>
    public bool IsCompressed => CompressedClusters != 0;

    private int GetSaveLength(long previousLcn)
    {
        var deltaLcn = LCN == 0 ? 0 : LCN - previousLcn;

        var offsetBytes = CalculateBytesNecessary(deltaLcn);
        var lengthBytes = CalculateBytesNecessary(Clusters);

        var nextBytes = 0;
        if (IsCompressed)
        {
            nextBytes = 1 + CalculateBytesNecessary(CompressedClusters);
        }

        return 1 + offsetBytes + lengthBytes + nextBytes;
    }

    private void Save(byte[] buffer, int offset, long previousLcn)
    {
        var deltaLcn = LCN == 0 ? 0 : LCN - previousLcn;
        var length = Clusters;

        var offsetBytes = CalculateBytesNecessary(deltaLcn);
        var lengthBytes = CalculateBytesNecessary(length);

        buffer[offset] = (byte)((offsetBytes << 4) | lengthBytes);

        for (var i = 0; i < lengthBytes; i++)
        {
            buffer[offset + 1 + i] = (byte)(length & 0xFF);
            length >>= 8;
        }

        for (var i = 0; i < offsetBytes; i++)
        {
            buffer[offset + 1 + lengthBytes + i] = (byte)(deltaLcn & 0xFF);
            deltaLcn >>= 8;
        }

        // Make followup compressed cluster
        if (IsCompressed)
        {
            length = CompressedClusters;
            lengthBytes = CalculateBytesNecessary(length);

            buffer[offset + 1 + lengthBytes + offsetBytes] = lengthBytes;

            for (var i = 0; i < lengthBytes; i++)
            {
                buffer[offset + 1 + lengthBytes + offsetBytes + 1 + i] = (byte)(length & 0xFF);
                length >>= 8;
            }
        }
    }

    private static byte CalculateBytesNecessary(long value)
    {
        if (value == 0)
        {
            return 0;
        }

        var isNegative = false;
        if (value < 0)
        {
            value = -value;
            isNegative = true;
        }

        var tester = 0x80L;
        for (byte i = 0; i < 8; i++)
        {
            if (tester > value)
            {
                return (byte)(i + 1);
            }

            tester <<= 8;
        }

        throw new Exception();
    }

    public static int GetSaveLength(List<DataFragment> fragments)
    {
        long lcn = 0;
        var saveLength = 0;

        foreach (var fragment in fragments)
        {
            saveLength += fragment.GetSaveLength(lcn);

            if (fragment.LCN != 0)
            {
                lcn = fragment.LCN;
            }
        }

        return saveLength;
    }

    public static void Save(byte[] buffer, int offset, List<DataFragment> fragments)
    {
        long lcn = 0;
        var pointer = offset;

        foreach (var fragment in fragments)
        {
            var saveLength = fragment.GetSaveLength(lcn);
            fragment.Save(buffer, pointer, lcn);

            pointer += saveLength;
            if (fragment.LCN != 0)
            {
                lcn = fragment.LCN;
            }
        }
    }

    public static DataFragment ParseFragment(byte[] data, long previousLcn, int offset)
    {
        var res = new DataFragment();

        res.FragmentSizeBytes = data[offset];
        var offsetBytes = (byte)(res.FragmentSizeBytes >> 4);
        var countBytes = (byte)(res.FragmentSizeBytes & 0x0F);

        res.ThisObjectLength = 1 + countBytes + offsetBytes;

        if (countBytes == 0)
        {
            res.FragmentSizeBytes = 0;
            return res;
        }

        offsetBytes = (byte)(offsetBytes & 0xF7); // 0xF7: 1111 0111
        //// Debug.Assert(countBytes <= 8, "Fragment metadata exceeded 8 bytes");
        //// Debug.Assert(offsetBytes <= 8, "Fragment metadata exceeded 8 bytes");

        var tmpData = new byte[8];
        Array.Copy(data, offset + 1, tmpData, 0, countBytes);

        res.Clusters = BitConverter.ToInt64(tmpData, 0);
        res.LCN = previousLcn;

        if (offsetBytes == 0)
        {
            // Sparse chunk
            res.LCN = 0;
        }
        else
        {
            long deltaLcn = 0;

            for (var i = offsetBytes - 1; i >= 0; i--)
            {
                deltaLcn = deltaLcn << 8;
                deltaLcn += data[offset + 1 + countBytes + i];
            }

            // Is negative?
            var negativeValue = (long)128 << (8 * (offsetBytes - 1));
            if ((deltaLcn & negativeValue) == negativeValue)
            {
                // Negtive
                // Set the remaining bytes to 0xFF
                long tmp = 0xFF;
                for (var i = 0; i < 8 - offsetBytes; i++)
                {
                    tmp = tmp << 8;
                    tmp |= 0xFF;
                }

                for (var i = 8 - offsetBytes; i < 8; i++)
                {
                    tmp = tmp << 8;
                }

                deltaLcn |= tmp;
            }

            res.LCN = res.LCN + deltaLcn;
        }

        return res;
    }

    public static List<DataFragment> ParseFragments(byte[] data, int maxLength, int offset, long startingVCN,
        long endingVCN)
    {
        //// Debug.Assert(data.Length - offset >= maxLength);
        //// Debug.Assert(0 <= offset && offset <= data.Length);

        var fragments = new List<DataFragment>();

        var vcn = startingVCN;

        var pointer = offset;
        long lastLcn = 0;
        while (pointer < offset + maxLength)
        {
            // // Debug.Assert(pointer <= offset + maxLength);

            var fragment = ParseFragment(data, lastLcn, pointer);

            pointer += fragment.ThisObjectLength;

            if (fragment.FragmentSizeBytes == 0)
                // Last fragment
            {
                break;
            }

            fragment.StartingVCN = vcn;

            vcn += fragment.Clusters;

            if (!fragment.IsSparseFragment)
                // Don't count sparse fragments for offsets
            {
                lastLcn = fragment.LCN;
            }

            fragments.Add(fragment);
        }

        // Checks
        //// Debug.Assert(fragments.Count == 0 || startingVCN == fragments[0].StartingVCN);
        //// Debug.Assert(endingVCN == vcn - 1);

        // Return
        return fragments;
    }

    public static void CompactCompressedFragments(List<DataFragment> fragments)
    {
        for (var i = 0; i < fragments.Count - 1; i++)
        {
            if (fragments[i + 1].IsSparseFragment &&
                (fragments[i].Clusters + fragments[i + 1].Clusters) % 16 == 0 &&
                fragments[i + 1].Clusters < 16)
            {
                // Compact
                fragments[i].CompressedClusters = (byte)fragments[i + 1].Clusters;
                fragments.RemoveAt(i + 1);

                i--;
            }
        }
    }

    public static void CompactFragmentList(List<DataFragment> fragments)
    {
        for (var j = 0; j < fragments.Count - 1; j++)
        {
            if (!fragments[j].IsCompressed && !fragments[j].IsSparseFragment &&
                !fragments[j + 1].IsCompressed && !fragments[j + 1].IsSparseFragment &&
                fragments[j].LCN + fragments[j].Clusters == fragments[j + 1].LCN)
            {
                // Compact
                fragments[j].Clusters += fragments[j + 1].Clusters;

                fragments.RemoveAt(j + 1);

                j--;
            }
        }
    }
}
