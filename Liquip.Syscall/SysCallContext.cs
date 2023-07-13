using System.Runtime.InteropServices;

namespace Liquip.Syscall;

[StructLayout(LayoutKind.Explicit, Size = 80)]
public struct SysCallContext {
    [FieldOffset(0)]
    public uint CallerId;

    [FieldOffset(4)]
    public uint EDI;

    [FieldOffset(8)]
    public uint ESI;

    [FieldOffset(12)]
    public uint EBP;

    [FieldOffset(16)]
    public uint ESP;

    [FieldOffset(20)]
    public uint EBX;

    [FieldOffset(24)]
    public uint EDX;

    [FieldOffset(28)]
    public uint ECX;

    [FieldOffset(32)]
    public uint EAX;

    [FieldOffset(36)]
    public uint UserESP;
}
