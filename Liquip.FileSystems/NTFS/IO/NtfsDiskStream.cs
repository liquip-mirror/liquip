using Liquip.FileSystems.NTFS.Compression;
using Liquip.FileSystems.NTFS.Model;
using Liquip.FileSystems.NTFS.Utility;

namespace Liquip.FileSystems.NTFS.IO;

public class NtfsDiskStream : Stream
{
    private readonly uint _bytesPrCluster;
    private readonly ushort _compressionClusterCount;
    private readonly LZNT1? _compressor;

    private readonly Stream _diskStream;
    private readonly List<DataFragment> _fragments;
    private readonly long _length;
    private readonly bool _ownsStream;
    private long _position;

    public NtfsDiskStream(Stream diskStream, bool ownsStream, List<DataFragment> fragments, uint bytesPrCluster,
        ushort compressionClusterCount, long length)
    {
        _diskStream = diskStream;
        _ownsStream = ownsStream;
        _bytesPrCluster = bytesPrCluster;
        _compressionClusterCount = compressionClusterCount;

        _fragments = Util.Sort(fragments, new DataFragmentComparer());

        _length = length;
        _position = 0;

        if (compressionClusterCount != 0)
        {
            _compressor = new LZNT1();
            _compressor.BlockSize = (int)_bytesPrCluster;
        }

        long vcn = 0;
        var hasCompression = false;
        for (var i = 0; i < _fragments.Count; i++)
        {
            if (_fragments[i].IsCompressed)
            {
                hasCompression = true;
            }

            // Debug.Assert(_fragments[i].StartingVCN == vcn);
            vcn += _fragments[i].Clusters + _fragments[i].CompressedClusters;
        }

        //if (_compressionClusterCount == 0)
        // Debug.Assert(!hasCompression);
    }

    private bool IsEof => _position >= _length;

    public override bool CanRead => true;

    public override bool CanSeek => true;

    public override bool CanWrite =>
        // Not implemented
        false;

    public override long Length => _length;

    public override long Position
    {
        get => _position;
        set
        {
            if (value < 0 || _length < value)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            _position = value;
        }
    }

    public override void Flush()
    {
        throw new NotImplementedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        var newPosition = offset;

        switch (origin)
        {
            case SeekOrigin.Begin:
                newPosition = offset;
                break;
            case SeekOrigin.Current:
                newPosition += offset;
                break;
            case SeekOrigin.End:
                newPosition = _length + offset;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(origin));
        }

        if (newPosition < 0 || _length < newPosition)
        {
            throw new ArgumentOutOfRangeException(nameof(origin));
        }

        // Set 
        _position = newPosition;

        return _position;
    }

    public override void SetLength(long value)
    {
        throw new InvalidOperationException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var totalRead = 0;

        // Determine fragment
        while (count > 0 && _position < _length)
        {
            long fragmentOffset;
            var fragment = FindFragment(_position, out fragmentOffset);

            var diskOffset = fragment.LCN * _bytesPrCluster;
            var fragmentLength = fragment.Clusters * _bytesPrCluster;

            int actualRead;
            if (fragment.IsCompressed)
            {
                // Read and decompress
                var compressedData = new byte[fragmentLength];
                _diskStream.Seek(diskOffset, SeekOrigin.Begin);
                _diskStream.Read(compressedData, 0, compressedData.Length);

                var decompressedLength = (int)((fragment.Clusters + fragment.CompressedClusters) * _bytesPrCluster);
                var toRead = (int)Math.Min(decompressedLength - fragmentOffset,
                    Math.Min(_length - _position, count));

                // Debug.Assert(decompressedLength == _compressionClusterCount * _bytesPrCluster);

                if (fragmentOffset == 0 && toRead == decompressedLength)
                {
                    // Decompress directly (we're in the middle of a file and reading a full 16 clusters out)
                    actualRead = _compressor.Decompress(compressedData, 0, compressedData.Length, buffer, offset);
                }
                else
                {
                    // Decompress temporarily
                    var tmp = new byte[decompressedLength];
                    var decompressed = _compressor.Decompress(compressedData, 0, compressedData.Length, tmp, 0);

                    toRead = Math.Min(toRead, decompressed);

                    // Copy wanted data
                    Array.Copy(tmp, fragmentOffset, buffer, offset, toRead);

                    actualRead = toRead;
                }
            }
            else if (fragment.IsSparseFragment)
            {
                // Fill with zeroes
                // How much to fill?
                var toFill = (int)Math.Min(fragmentLength - fragmentOffset, count);

                Array.Clear(buffer, offset, toFill);

                actualRead = toFill;
            }
            else
            {
                // Read directly
                // How much can we read?
                var toRead = (int)Math.Min(fragmentLength - fragmentOffset, Math.Min(_length - _position, count));

                // Read it
                _diskStream.Seek(diskOffset + fragmentOffset, SeekOrigin.Begin);
                actualRead = _diskStream.Read(buffer, offset, toRead);
            }

            // Increments
            count -= actualRead;
            offset += actualRead;

            _position += actualRead;

            totalRead += actualRead;

            // Check
            if (actualRead == 0)
            {
                break;
            }
        }

        return totalRead;
    }

    private DataFragment FindFragment(long fileIndex, out long offsetInFragment)
    {
        for (var i = 0; i < _fragments.Count; i++)
        {
            var fragmentStart = _fragments[i].StartingVCN * _bytesPrCluster;
            var fragmentEnd = fragmentStart +
                              (_fragments[i].Clusters + _fragments[i].CompressedClusters) * _bytesPrCluster;

            if (fragmentStart <= fileIndex && fileIndex < fragmentEnd)
            {
                // Found
                offsetInFragment = fileIndex - fragmentStart;

                return _fragments[i];
            }
        }

        offsetInFragment = -1;

        return null;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        // Not implemented
        throw new InvalidOperationException();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (_ownsStream)
        {
            _diskStream.Dispose();
        }
    }

    public override void Close()
    {
        base.Close();

        if (_ownsStream)
        {
            _diskStream.Close();
        }
    }
}
