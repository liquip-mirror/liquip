using System.Runtime.InteropServices;

namespace Cosmos.Limine.Struct;

// typedef void (*limine_terminal_callback)(struct limine_terminal *, uint64_t, uint64_t, uint64_t, uint64_t);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void limine_terminal_callback(
    [MarshalAs(UnmanagedType.LPStruct)] LimineTerminal t, 
    ulong a, 
    ulong b, 
    ulong c, 
    ulong d
);

// typedef void (*limine_terminal_write)(struct limine_terminal *terminal, const char *string, uint64_t length);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void limine_terminal_write(
    [MarshalAs(UnmanagedType.LPStruct)] LimineTerminal t, 
    [MarshalAs(UnmanagedType.LPStr)] string data, 
    ulong length
);


[StructLayout(LayoutKind.Sequential)]
public struct TerminalRequest
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ulong[] id;
    public ulong revision;
    [MarshalAs(UnmanagedType.LPStruct)]
    public TerminalResponce response;
    public limine_terminal_callback callback;
}


[StructLayout(LayoutKind.Sequential)]
public struct TerminalResponce
{
    public ulong revision;
    public ulong terminal_count;
    [MarshalAs(UnmanagedType.LPStruct)]
    public LimineTerminal[] terminals;
    public limine_terminal_write write;
}

[StructLayout(LayoutKind.Sequential)]
public struct LimineTerminal
{
    public ulong columns;
    public ulong rows;
    [MarshalAs(UnmanagedType.LPStruct)]
    public Framebuffer framebuffer;
}