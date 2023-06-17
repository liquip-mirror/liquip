﻿using System;
using Zarlo.Cosmos.FileSystems.NTFS.Model.Enums;

namespace Zarlo.Cosmos.FileSystems.NTFS.Model.Attributes
{
    public class AttributeGeneric : Attribute
    {
        public byte[] Data { get; set; }

        public override AttributeResidentAllow AllowedResidentStates
        {
            get { return AttributeResidentAllow.Resident; }
        }

        internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
        {
            base.ParseAttributeResidentBody(data, maxLength, offset);

            // Get data
            Data = new byte[maxLength];
            Array.Copy(data, offset, Data, 0, maxLength);
        }
    }
}