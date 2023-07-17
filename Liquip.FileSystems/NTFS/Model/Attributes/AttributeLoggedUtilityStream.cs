﻿using Liquip.FileSystems.NTFS.Model.Enums;

namespace Liquip.FileSystems.NTFS.Model.Attributes;

public class AttributeLoggedUtilityStream : Attribute
{
    public byte[] Data { get; set; }

    public override AttributeResidentAllow AllowedResidentStates => AttributeResidentAllow.Resident;

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        // Debug.Assert(maxLength >= 1);

        Data = new byte[maxLength];
        Array.Copy(data, offset, Data, 0, maxLength);
    }
}