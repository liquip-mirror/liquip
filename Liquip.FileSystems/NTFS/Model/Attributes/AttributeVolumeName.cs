using System.Text;
using Liquip.FileSystems.NTFS.Model.Enums;

namespace Liquip.FileSystems.NTFS.Model.Attributes;

public class AttributeVolumeName : Attribute
{
    public string VolumeName { get; set; }

    public override AttributeResidentAllow AllowedResidentStates => AttributeResidentAllow.Resident;

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        // Debug.Assert(maxLength >= ResidentHeader.ContentLength);

        VolumeName = Encoding.Unicode.GetString(data, offset, (int)ResidentHeader.ContentLength);
    }
}
