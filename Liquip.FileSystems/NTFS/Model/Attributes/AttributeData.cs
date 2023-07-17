﻿using Liquip.FileSystems.NTFS.Model.Enums;

namespace Liquip.FileSystems.NTFS.Model.Attributes;

public class AttributeData : Attribute
{
    /// <summary>
    ///     If NonResidentFlag == ResidentFlag.Resident, then DataBytes has all the data of the entry
    /// </summary>
    public byte[] DataBytes { get; set; }

    /// <summary>
    ///     If NonResidentFlag == ResidentFlag.NonResident, then the DataFragments property describes all data fragments
    /// </summary>
    public List<DataFragment> DataFragments => NonResidentHeader.Fragments;

    public override AttributeResidentAllow AllowedResidentStates =>
        AttributeResidentAllow.Resident | AttributeResidentAllow.NonResident;

    internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
    {
        base.ParseAttributeResidentBody(data, maxLength, offset);

        DataBytes = new byte[ResidentHeader.ContentLength];
        Array.Copy(data, offset, DataBytes, 0, DataBytes.Length);
    }
}