using System.Runtime.InteropServices;

namespace Liquip.Threading.Paging.Struct;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct PagingTableEntry
{
    public uint Raw;

    public bool P {
        get => Utils.Has.Flag(Raw, 0);
        set => Utils.Set.Flag(ref Raw, 0, value);
    }

    public bool RW {
        get => Utils.Has.Flag(Raw, 1);
        set => Utils.Set.Flag(ref Raw, 1, value);
    }

    public bool US {
        get => Utils.Has.Flag(Raw, 2);
        set => Utils.Set.Flag(ref Raw, 2, value);
    }

    public bool PWT {
        get => Utils.Has.Flag(Raw, 3);
        set => Utils.Set.Flag(ref Raw, 3, value);
    }

    public bool PCD {
        get => Utils.Has.Flag(Raw, 4);
        set => Utils.Set.Flag(ref Raw, 4, value);
    }

    public bool A {
        get => Utils.Has.Flag(Raw, 5);
        set => Utils.Set.Flag(ref Raw, 5, value);
    }

    public bool AVL {
        get => Utils.Has.Flag(Raw, 6);
        set => Utils.Set.Flag(ref Raw, 6, value);
    }

    public bool PS {
        get => Utils.Has.Flag(Raw, 7);
        set => Utils.Set.Flag(ref Raw, 7, value);
    }

    public bool PAT {
        get => Utils.Has.Flag(Raw, 12);
        set => Utils.Set.Flag(ref Raw, 12, value);
    }

}
