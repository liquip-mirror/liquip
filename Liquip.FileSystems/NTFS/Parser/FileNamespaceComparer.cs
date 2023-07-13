using Liquip.FileSystems.NTFS.Model.Enums;

namespace Liquip.FileSystems.NTFS.Parser;

public class FileNamespaceComparer : IComparer<FileNamespace>
{
    private static readonly FileNamespace[] _order =
    {
        FileNamespace.Win32, FileNamespace.Win32AndDOS, FileNamespace.POSIX, FileNamespace.DOS
    };

    public int Compare(FileNamespace x, FileNamespace y)
    {
        foreach (var fileNamespace in _order)
        {
            if (x == fileNamespace && y == fileNamespace)
            {
                return 0;
            }

            if (x == fileNamespace)
            {
                return 1;
            }

            if (y == fileNamespace)
            {
                return -1;
            }
        }

        return 0;
    }
}
