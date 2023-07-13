using Liquip.FileSystems.NTFS.Model;

namespace Liquip.FileSystems.NTFS.IO;

public class DataFragmentComparer : IComparer<DataFragment>
{
    public int Compare(DataFragment? x, DataFragment? y)
    {
        if (x != null && y != null)
        {
            return x.StartingVCN.CompareTo(y.StartingVCN);
        }

        return 0;
    }
}
