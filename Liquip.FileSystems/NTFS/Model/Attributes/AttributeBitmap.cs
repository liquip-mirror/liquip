using System.Collections;
using Liquip.FileSystems.NTFS.Model.Enums;
using Liquip.FileSystems.NTFS.Utility;

namespace Liquip.FileSystems.NTFS.Model.Attributes;

public class AttributeBitmap : Attribute
{
    public BitArray Bitfield { get; set; }

    public override AttributeResidentAllow AllowedResidentStates =>
        AttributeResidentAllow.Resident | AttributeResidentAllow.NonResident;

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        // Debug.Assert(maxLength >= 1);

        var tmpData = new byte[maxLength];
        Array.Copy(data, offset, tmpData, 0, maxLength);

        Bitfield = new BitArray(tmpData);
    }

    internal override void ParseAttributeNonResidentBody(Ntfs ntfsInfo)
    {
        base.ParseAttributeNonResidentBody(ntfsInfo);

        // Get all chunks
        var data = NtfsUtils.ReadFragments(ntfsInfo, NonResidentHeader.Fragments);

        // Parse
        Bitfield = new BitArray(data);
    }
}
