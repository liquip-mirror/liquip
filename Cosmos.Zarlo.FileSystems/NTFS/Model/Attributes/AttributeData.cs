﻿using System;
using System.Collections.Generic;
using Cosmos.Zarlo.FileSystems.NTFS.Model.Enums;

namespace Cosmos.Zarlo.FileSystems.NTFS.Model.Attributes
{
    public class AttributeData : Attribute
    {
        /// <summary>
        /// If NonResidentFlag == ResidentFlag.Resident, then DataBytes has all the data of the entry
        /// </summary>
        public byte[] DataBytes { get; set; }

        /// <summary>
        /// If NonResidentFlag == ResidentFlag.NonResident, then the DataFragments property describes all data fragments
        /// </summary>
        public List<DataFragment> DataFragments
        {
            get { return NonResidentHeader.Fragments; }
        }

        public override AttributeResidentAllow AllowedResidentStates
        {
            get
            {
                return AttributeResidentAllow.Resident | AttributeResidentAllow.NonResident;
            }
        }

        internal override void ParseAttributeResidentBody(byte[] data, int maxLength, int offset)
        {
            base.ParseAttributeResidentBody(data, maxLength, offset);

            DataBytes = new byte[ResidentHeader.ContentLength];
            Array.Copy(data, offset, DataBytes, 0, DataBytes.Length);
        }
    }
}