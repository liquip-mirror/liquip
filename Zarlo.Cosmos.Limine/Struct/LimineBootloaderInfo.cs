using System.Runtime.InteropServices;

namespace Zarlo.Cosmos.Limine.Struct;

[StructLayout(LayoutKind.Sequential)]
public struct LimineBootloaderInfoRequest
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ulong[] id;

    public ulong revision;
    [MarshalAs(UnmanagedType.LPStruct)] unsafe public LimineBootloaderInfoResponse response;
}

[StructLayout(LayoutKind.Sequential)]
public struct LimineBootloaderInfoResponse
{
    public ulong revision;
    [MarshalAs(UnmanagedType.LPTStr)] public char[] name;

    [MarshalAs(UnmanagedType.LPTStr)] public char[] version;
}