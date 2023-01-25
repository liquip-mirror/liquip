using System.Drawing;
using System.Reflection.Metadata;
using Cosmos.Zarlo.Limine.Struct;

namespace Cosmos.Zarlo.Limine;

public class FramebufferObject
{

    public class Mode {
        public Mode(VideoMode mode)
        {
            Pitch = mode.pitch; 
            Width = mode.width; 
            Height = mode.height; 
            BPP = mode.bpp; 
            MemoryModel = mode.memory_model; 
            RedMaskSize = mode.red_mask_size; 
            RedMaskShift = mode.red_mask_shift; 
            GreenMaskSize = mode.green_mask_size; 
            GreenMaskShift = mode.green_mask_shift; 
            BlueMaskSize = mode.blue_mask_size; 
            BlueMaskShift = mode.blue_mask_shift; 
        }
        public ulong Pitch;
        public ulong Width;
        public ulong Height;
        public ulong BPP;
        public byte MemoryModel;
        public byte RedMaskSize;
        public byte RedMaskShift;
        public byte GreenMaskSize;
        public byte GreenMaskShift;
        public byte BlueMaskSize;
        public byte BlueMaskShift;
    }

    private readonly int _id;
    private readonly Framebuffer _framebuffer;

    public FramebufferObject(int id)
    {
        _id = id;
        _framebuffer = framebufferRequest.response.framebuffers[id];
    }

    public FramebufferObject(Framebuffer framebuffer)
    {
        _framebuffer = framebuffer;
    }

    public static FramebufferRequest GetRaw => framebufferRequest;

    private static FramebufferRequest framebufferRequest = new FramebufferRequest() {
        id = new ulong[] {
            0xc7b1dd30df4c8b88, 
            0x0a82e883a194f07b,
            0x9d5827dcd881dd75,
            0xa3148604f6fab11b
        },
        revision = 0
    };

    public List<Mode> GetModes()
    {
        var output = new List<Mode>();

        for (int i = 0; i < (int)_framebuffer.mode_count; i++)
        {
            output.Add(new Mode(_framebuffer.modes[i]));
        }

        return output;
    }

    public void CopyFrom(uint x, uint y, ref int[] buffer)
    {
        var offset = x + (y * _framebuffer.pitch);
        unsafe
        {
            fixed(int* ptr = buffer)
            Buffer.MemoryCopy(_framebuffer.address + offset, ptr, buffer.Length * 4, buffer.Length * 4);
        }
    }

    public void CopyTo(uint x, uint y, ref int[] buffer)
    {
        var offset = x + (y * _framebuffer.pitch);
        unsafe
        {
            fixed(int* ptr = buffer)
            Buffer.MemoryCopy(ptr, _framebuffer.address + offset, buffer.Length * 4, buffer.Length * 4);
        }
    }

}
