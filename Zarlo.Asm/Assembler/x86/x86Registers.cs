namespace Zarlo.Asm.Assembler.x86;

public static class x86Registers
{

    public static List<x86Register> Allx86Registers = new List<x86Register>();

    public static x86Register? Get(string name)
    { 
        foreach (var item in Allx86Registers)
        {
            if (item.Name() == name)
            {
                return item;
            }
        }
        return null;
    }

    public static List<x86Register> Get(byte b)
    {
        var output = new List<x86Register>();
        foreach (var item in Allx86Registers)
        {
            if (item.Code() == b)
            {
                output.Add(item);
            }
        }
        return output;
    }

    public static void Register(x86Register register)
    {
        Allx86Registers.Add(register);
    }

    // Segment registers
    public static readonly x86Register CS = new x86Register(nameof(CS), 16, 0b000);
    public static readonly x86Register DS = new x86Register(nameof(DS), 16, 0b000);
    public static readonly x86Register SS = new x86Register(nameof(SS), 16, 0b000);
    public static readonly x86Register ES = new x86Register(nameof(ES), 16, 0b000);
    public static readonly x86Register FS = new x86Register(nameof(FS), 16, 0b000);
    public static readonly x86Register GS = new x86Register(nameof(GS), 16, 0b000);
    // End Segment registers

    // Accumulator
    public static readonly x86Register AL = new x86Register(nameof(AL),   8,  0b000);
    public static readonly x86Register AH = new x86Register(nameof(AH),   8,  0b100);
    public static readonly x86Register AX = new x86Register(nameof(AX),   16, 0b000, nameof(AL), nameof(AH));
    public static readonly x86Register EAX = new x86Register(nameof(EAX), 32, 0b000, nameof(AL), nameof(AH), nameof(AX));
    // End Accumulator

    // Base index
    public static readonly x86Register BL = new x86Register(nameof(BL),   8,  0b011);
    public static readonly x86Register BH = new x86Register(nameof(BH),   8,  0b111);
    public static readonly x86Register BX = new x86Register(nameof(BX),   16, 0b011, nameof(BL), nameof(BH));
    public static readonly x86Register EBX = new x86Register(nameof(EBX), 32, 0b011, nameof(BL), nameof(BH), nameof(BX));
    // End Base index

    // Counter
    public static readonly x86Register CL = new x86Register(nameof(CL),   8,  0b001);
    public static readonly x86Register CH = new x86Register(nameof(CH),   8,  0b101);
    public static readonly x86Register CX = new x86Register(nameof(CX),   16, 0b001, nameof(CL), nameof(CH));
    public static readonly x86Register ECX = new x86Register(nameof(ECX), 32, 0b001, nameof(CL), nameof(CH), nameof(CX));
    // End Counter

    // Extend accumulator
    public static readonly x86Register DL = new x86Register(nameof(DL),   8,  0b010);
    public static readonly x86Register DH = new x86Register(nameof(DH),   8,  0b110);
    public static readonly x86Register DX = new x86Register(nameof(DX),   16, 0b010, nameof(DL), nameof(DH));
    public static readonly x86Register EDX = new x86Register(nameof(EDX), 32, 0b010, nameof(DL), nameof(DH), nameof(DX));
    // End Extend accumulator

    // Stack pointer
    public static readonly x86Register SP  = new x86Register(nameof(SP),  16, 0b100);
    public static readonly x86Register ESP = new x86Register(nameof(ESP), 32, 0b100, nameof(SP));
    // End Stack pointer

    // Stack base pointer
    public static readonly x86Register BP  = new x86Register(nameof(BP),  16, 0b101);
    public static readonly x86Register EBP = new x86Register(nameof(EBP), 32, 0b101, nameof(BP));
    // End Stack base pointer

    // Source index
    public static readonly x86Register SI = new x86Register(nameof(SI),   16, 0b110);
    public static readonly x86Register ESI = new x86Register(nameof(ESI), 32, 0b110, nameof(SI));
    // End Source index

    // Destination index
    public static readonly x86Register DI = new x86Register(nameof(DI),   16, 0b111);
    public static readonly x86Register EDI = new x86Register(nameof(EDI), 32, 0b111, nameof(DI));
    // End Destination index

    // Instruction pointer
    public static readonly x86Register IP = new x86Register(nameof(IP),   16, 0b000);
    public static readonly x86Register EIP = new x86Register(nameof(EIP), 32, 0b000, nameof(IP));
    // End Instruction pointer

    // SIMD
    public static readonly x86Register MMX0  = new x86Register(nameof(MMX0),  64, 0b000);
    public static readonly x86Register MMX1  = new x86Register(nameof(MMX1),  64, 0b000);
    public static readonly x86Register MMX2  = new x86Register(nameof(MMX2),  64, 0b000);
    public static readonly x86Register MMX3  = new x86Register(nameof(MMX3),  64, 0b000);
    public static readonly x86Register MMX4  = new x86Register(nameof(MMX4),  64, 0b000);
    public static readonly x86Register MMX5  = new x86Register(nameof(MMX5),  64, 0b000);
    public static readonly x86Register MMX6  = new x86Register(nameof(MMX6),  64, 0b000);
    public static readonly x86Register MMX7  = new x86Register(nameof(MMX7),  64, 0b000);
    public static readonly x86Register MMX8  = new x86Register(nameof(MMX8),  64, 0b000);
    public static readonly x86Register MMX9  = new x86Register(nameof(MMX9),  64, 0b000);
    public static readonly x86Register MMX10 = new x86Register(nameof(MMX10), 64, 0b000);
    public static readonly x86Register MMX11 = new x86Register(nameof(MMX11), 64, 0b000);
    public static readonly x86Register MMX12 = new x86Register(nameof(MMX12), 64, 0b000);
    public static readonly x86Register MMX13 = new x86Register(nameof(MMX13), 64, 0b000);
    public static readonly x86Register MMX14 = new x86Register(nameof(MMX14), 64, 0b000);
    public static readonly x86Register MMX15 = new x86Register(nameof(MMX15), 64, 0b000);
    // End SIMD

    // SIMD
    public static readonly x86Register XMM0  = new x86Register(nameof(XMM0),  128, 0b000);
    public static readonly x86Register XMM1  = new x86Register(nameof(XMM1),  128, 0b000);
    public static readonly x86Register XMM2  = new x86Register(nameof(XMM2),  128, 0b000);
    public static readonly x86Register XMM3  = new x86Register(nameof(XMM3),  128, 0b000);
    public static readonly x86Register XMM4  = new x86Register(nameof(XMM4),  128, 0b000);
    public static readonly x86Register XMM5  = new x86Register(nameof(XMM5),  128, 0b000);
    public static readonly x86Register XMM6  = new x86Register(nameof(XMM6),  128, 0b000);
    public static readonly x86Register XMM7  = new x86Register(nameof(XMM7),  128, 0b000);
    public static readonly x86Register XMM8  = new x86Register(nameof(XMM8),  128, 0b000);
    public static readonly x86Register XMM9  = new x86Register(nameof(XMM9),  128, 0b000);
    public static readonly x86Register XMM10 = new x86Register(nameof(XMM10), 128, 0b000);
    public static readonly x86Register XMM11 = new x86Register(nameof(XMM11), 128, 0b000);
    public static readonly x86Register XMM12 = new x86Register(nameof(XMM12), 128, 0b000);
    public static readonly x86Register XMM13 = new x86Register(nameof(XMM13), 128, 0b000);
    public static readonly x86Register XMM14 = new x86Register(nameof(XMM14), 128, 0b000);
    public static readonly x86Register XMM15 = new x86Register(nameof(XMM15), 128, 0b000);
    // End SIMD

    // SIMD
    public static readonly x86Register YMM0  = new x86Register(nameof(YMM0),  256, 0b000, nameof(XMM0));
    public static readonly x86Register YMM1  = new x86Register(nameof(YMM1),  256, 0b000, nameof(XMM1));
    public static readonly x86Register YMM2  = new x86Register(nameof(YMM2),  256, 0b000, nameof(XMM2));
    public static readonly x86Register YMM3  = new x86Register(nameof(YMM3),  256, 0b000, nameof(XMM3));
    public static readonly x86Register YMM4  = new x86Register(nameof(YMM4),  256, 0b000, nameof(XMM4));
    public static readonly x86Register YMM5  = new x86Register(nameof(YMM5),  256, 0b000, nameof(XMM5));
    public static readonly x86Register YMM6  = new x86Register(nameof(YMM6),  256, 0b000, nameof(XMM6));
    public static readonly x86Register YMM7  = new x86Register(nameof(YMM7),  256, 0b000, nameof(XMM7));
    public static readonly x86Register YMM8  = new x86Register(nameof(YMM8),  256, 0b000, nameof(XMM8));
    public static readonly x86Register YMM9  = new x86Register(nameof(YMM9),  256, 0b000, nameof(XMM9));
    public static readonly x86Register YMM10 = new x86Register(nameof(YMM10), 256, 0b000, nameof(XMM10));
    public static readonly x86Register YMM11 = new x86Register(nameof(YMM11), 256, 0b000, nameof(XMM11));
    public static readonly x86Register YMM12 = new x86Register(nameof(YMM12), 256, 0b000, nameof(XMM12));
    public static readonly x86Register YMM13 = new x86Register(nameof(YMM13), 256, 0b000, nameof(XMM13));
    public static readonly x86Register YMM14 = new x86Register(nameof(YMM14), 256, 0b000, nameof(XMM14));
    public static readonly x86Register YMM15 = new x86Register(nameof(YMM15), 256, 0b000, nameof(XMM15));
    // End SIMD

}
