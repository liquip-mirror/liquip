using Liquip.FileSystems.NTFS.Model.Enums;
using Liquip.FileSystems.NTFS.Utility;

namespace Liquip.FileSystems.NTFS.Model.Attributes;

public class AttributeExtendedAttributes : Attribute
{
    public List<ExtendedAttribute> ExtendedAttributes { get; set; }

    public override AttributeResidentAllow AllowedResidentStates =>
        AttributeResidentAllow.Resident | AttributeResidentAllow.NonResident;

    internal override void ParseAttributeNonResidentBody(Ntfs ntfsInfo)
    {
        base.ParseAttributeNonResidentBody(ntfsInfo);

        // Get all chunks
        var data = NtfsUtils.ReadFragments(ntfsInfo, NonResidentHeader.Fragments);

        // Parse
        // Debug.Assert(data.Length >= 8);

        var extendedAttributes = new List<ExtendedAttribute>();
        var pointer = 0;
        while (pointer + 8 <= data.Length) // 8 is the minimum size of an ExtendedAttribute
        {
            if (ExtendedAttribute.GetSize(data, pointer) <= 0)
            {
                break;
            }

            var ea = ExtendedAttribute.ParseData(data, (int)NonResidentHeader.ContentSize, pointer);

            extendedAttributes.Add(ea);

            pointer += ea.Size;
        }

        ExtendedAttributes = extendedAttributes;
    }

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        // Debug.Assert(maxLength >= 8);

        var extendedAttributes = new List<ExtendedAttribute>();
        var pointer = offset;
        while (pointer + 8 <= offset + maxLength) // 8 is the minimum size of an ExtendedAttribute
        {
            if (ExtendedAttribute.GetSize(data, pointer) < 0)
            {
                break;
            }

            var ea = ExtendedAttribute.ParseData(data, (int)ResidentHeader.ContentLength, pointer);

            extendedAttributes.Add(ea);

            pointer += ea.Size;
        }

        ExtendedAttributes = extendedAttributes;
    }
}
