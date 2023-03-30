﻿using System.Collections.Generic;
using Cosmos.Zarlo.FileSystems.NTFS.Model.Enums;

namespace Cosmos.Zarlo.FileSystems.NTFS.Parser
{
    public class FileNamespaceComparer : IComparer<FileNamespace>
    {
        static FileNamespace[] _order = new[]
            { FileNamespace.Win32, FileNamespace.Win32AndDOS, FileNamespace.POSIX, FileNamespace.DOS };

        public int Compare(FileNamespace x, FileNamespace y)
        {
            foreach (FileNamespace fileNamespace in _order)
            {
                if (x == fileNamespace && y == fileNamespace)
                    return 0;

                if (x == fileNamespace)
                    return 1;

                if (y == fileNamespace)
                    return -1;
            }

            return 0;
        }
    }
}