using System.Runtime.InteropServices;

namespace Liquip.Limine.Struct;

[StructLayout(LayoutKind.Sequential)]
public struct LimineBootloaderInfoRequest
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ulong[] id;

    public ulong revision;
    [MarshalAs(UnmanagedType.LPStruct)] public LimineBootloaderInfoResponse response;
}

[StructLayout(LayoutKind.Sequential)]
public struct LimineBootloaderInfoResponse
{
    public ulong revision;
    [MarshalAs(UnmanagedType.LPTStr)] public char[] name;

    [MarshalAs(UnmanagedType.LPTStr)] public char[] version;
}
