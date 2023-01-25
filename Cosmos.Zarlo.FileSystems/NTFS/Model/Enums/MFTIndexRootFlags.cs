﻿using System;

namespace Cosmos.Zarlo.FileSystems.NTFS.Model.Enums
{
    [Flags]
    public enum MFTIndexRootFlags
    {
        SmallIndex = 0x00,
        LargeIndex = 0x01
    }
}