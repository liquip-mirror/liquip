using System.Collections.Generic;
using Cosmos.Zarlo.FileSystems.NTFS.Model;

namespace Cosmos.Zarlo.FileSystems.NTFS.IO
{
    public class DataFragmentComparer : IComparer<DataFragment>
    {
        public int Compare(DataFragment x, DataFragment y)
        {
            if (x != null && y != null) return x.StartingVCN.CompareTo(y.StartingVCN);
            return 0;
        }
    }
}