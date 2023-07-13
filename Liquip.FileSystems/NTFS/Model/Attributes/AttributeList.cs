using Liquip.FileSystems.NTFS.Model.Enums;
using Liquip.FileSystems.NTFS.Utility;

namespace Liquip.FileSystems.NTFS.Model.Attributes;

public class AttributeList : Attribute
{
    public List<AttributeListItem> Items { get; set; }

    public override AttributeResidentAllow AllowedResidentStates =>
        AttributeResidentAllow.Resident | AttributeResidentAllow.NonResident;

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        // Debug.Assert(maxLength >= ResidentHeader.ContentLength);

        var results = new List<AttributeListItem>();

        var pointer = offset;
        while (pointer + 26 <= offset + maxLength) // 26 is the smallest possible MFTAttributeListItem
        {
            var item =
                AttributeListItem.ParseListItem(data, Math.Min(data.Length - pointer, maxLength), pointer);

            if (item.Type == AttributeType.EndOfAttributes)
            {
                break;
            }

            results.Add(item);

            pointer += item.Length;
        }

        Items = results;
    }

    internal override void ParseAttributeNonResidentBody(Ntfs ntfsInfo)
    {
        base.ParseAttributeNonResidentBody(ntfsInfo);

        // Get all chunks
        var data = NtfsUtils.ReadFragments(ntfsInfo, NonResidentHeader.Fragments);

        // Parse
        var results = new List<AttributeListItem>();

        var pointer = 0;
        var contentSize = (int)NonResidentHeader.ContentSize;
        while (pointer + 26 <= contentSize) // 26 is the smallest possible MFTAttributeListItem
        {
            var item = AttributeListItem.ParseListItem(data, data.Length - pointer, pointer);

            if (item.Type == AttributeType.EndOfAttributes)
            {
                break;
            }

            if (item.Length == 0)
            {
                break;
            }

            results.Add(item);

            pointer += item.Length;
        }

        // Debug.Assert(pointer == contentSize);

        Items = results;
    }
}
