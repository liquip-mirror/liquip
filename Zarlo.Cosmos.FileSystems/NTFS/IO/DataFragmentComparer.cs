using System.Collections.Generic;
using Zarlo.Cosmos.FileSystems.NTFS.Model;

namespace Zarlo.Cosmos.FileSystems.NTFS.IO;

public class DataFragmentComparer : IComparer<DataFragment>
{
    public int Compare(DataFragment? x, DataFragment? y)
    {
        if (x != null && y != null) return x.StartingVCN.CompareTo(y.StartingVCN);
        return 0;
    }
}