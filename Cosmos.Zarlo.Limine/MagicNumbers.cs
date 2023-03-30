namespace Cosmos.Zarlo.Limine;

public static class MagicNumbers
{
    public readonly static ulong[] Common = new ulong[2]
    {
        0xc7b1dd30df4c8b88,
        0x0a82e883a194f07b
    };

    public readonly static ulong[] BootloaderInfoRequest = new ulong[4]
    {
        Common[0],
        Common[1],
        0xf55038d8e2a1202f,
        0x279426fcf5f59740
    };

    public readonly static ulong[] StackSizeRequest = new ulong[4]
    {
        Common[0],
        Common[1],
        0x224ef0460a8e8926,
        0xe1cb0fc25f46ea3d
    };

    public readonly static ulong[] TerminalRequest = new ulong[4]
    {
        Common[0],
        Common[1],
        0xc8ac59310c2b0844,
        0xa68d0c7265d38878
    };

    public readonly static ulong[] FramebufferRequest = new ulong[4]
    {
        Common[0],
        Common[1],
        0x9d5827dcd881dd75,
        0xa3148604f6fab11b
    };
}