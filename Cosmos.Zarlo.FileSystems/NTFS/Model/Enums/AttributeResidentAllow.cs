using System;

namespace Cosmos.Zarlo.FileSystems.NTFS.Model.Enums
{
    [Flags]
    public enum AttributeResidentAllow
    {
        Resident = 1,
        NonResident = 2
    }
}