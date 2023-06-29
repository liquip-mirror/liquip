using Zarlo.Cosmos.FileSystems.NTFS.Model.Enums;

namespace Zarlo.Cosmos.FileSystems.NTFS.Model.Attributes;

public class AttributeIndexRoot : Attribute
{
    // Root
    public AttributeType IndexType { get; set; }
    public uint CollationRule { get; set; }
    public uint IndexAllocationSize { get; set; }
    public byte ClustersPrIndexRecord { get; set; }

    // Header
    public uint OffsetToFirstIndex { get; set; }
    public uint SizeOfIndexTotal { get; set; }
    public uint SizeOfIndexAllocated { get; set; }
    public MFTIndexRootFlags IndexFlags { get; set; }

    public List<IndexEntry> Entries { get; set; }

    public override AttributeResidentAllow AllowedResidentStates => AttributeResidentAllow.Resident;

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        // Debug.Assert(maxLength >= 32);

        IndexType = (AttributeType)BitConverter.ToUInt32(data, offset);
        CollationRule = BitConverter.ToUInt32(data, offset + 4);
        IndexAllocationSize = BitConverter.ToUInt32(data, offset + 8);
        ClustersPrIndexRecord = data[offset + 12];

        OffsetToFirstIndex = BitConverter.ToUInt32(data, offset + 16);
        SizeOfIndexTotal = BitConverter.ToUInt32(data, offset + 20);
        SizeOfIndexAllocated = BitConverter.ToUInt32(data, offset + 24);
        IndexFlags = (MFTIndexRootFlags)data[offset + 28];

        var entries = new List<IndexEntry>();

        // Parse entries
        var pointer = offset + 32;
        while (pointer <= offset + SizeOfIndexTotal + 32)
        {
            var entry = IndexEntry.ParseData(data, (int)SizeOfIndexTotal - (pointer - offset) + 32, pointer);

            if ((entry.Flags & MFTIndexEntryFlags.LastEntry) != 0)
            {
                break;
            }

            entries.Add(entry);

            pointer += entry.Size;
        }

        Entries = entries;
    }
}
