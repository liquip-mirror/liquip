using System.Runtime.InteropServices;

namespace Cosmos.Zarlo.Limine.Struct;

[StructLayout(LayoutKind.Sequential)]
public struct FramebufferRequest {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ulong[] id;
    public ulong revision;
    [MarshalAs(UnmanagedType.LPStruct)]
    public FramebufferResponse response;
};

[StructLayout(LayoutKind.Sequential)]
public struct FramebufferResponse {

    public ulong revision;
    public ulong framebuffer_count;
    [MarshalAs(UnmanagedType.LPStruct)]
    public Framebuffer[] framebuffers;
};


[StructLayout(LayoutKind.Sequential)]
public struct Framebuffer
{
    unsafe public int* address;

    public ulong width;
    public ulong height;
    public ulong pitch;
    public ushort bpp; // Bits per pixel
    public byte memory_model;
    public byte red_mask_size;
    public byte red_mask_shift;
    public byte green_mask_size;
    public byte green_mask_shift;
    public byte blue_mask_size;
    public byte blue_mask_shift;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public byte[] unused;
    public ulong edid_size;
    unsafe public int* edid;

    /* Revision 1 */
    public ulong mode_count;
    
    [MarshalAs(UnmanagedType.LPStruct)]
    public VideoMode[] modes;
}

[StructLayout(LayoutKind.Sequential)]
public struct VideoMode {
    public ulong pitch;
    public ulong width;
    public ulong height;
    public ulong bpp;
    public byte memory_model;
    public byte red_mask_size;
    public byte red_mask_shift;
    public byte green_mask_size;
    public byte green_mask_shift;
    public byte blue_mask_size;
    public byte blue_mask_shift;
}