namespace Zarlo.Asm.Assembler.x86;

public static class x86Registers
{
    public static List<x86Register> Allx86Registers = new();

    // Segment registers
    public static readonly x86Register CS = new(nameof(CS), 16, 0b000);
    public static readonly x86Register DS = new(nameof(DS), 16, 0b000);
    public static readonly x86Register SS = new(nameof(SS), 16, 0b000);
    public static readonly x86Register ES = new(nameof(ES), 16, 0b000);
    public static readonly x86Register FS = new(nameof(FS), 16, 0b000);

    public static readonly x86Register GS = new(nameof(GS), 16, 0b000);
    // End Segment registers

    // Accumulator
    public static readonly x86Register AL = new(nameof(AL), 8, 0b000);
    public static readonly x86Register AH = new(nameof(AH), 8, 0b100);
    public static readonly x86Register AX = new(nameof(AX), 16, 0b000, nameof(AL), nameof(AH));

    public static readonly x86Register EAX = new(nameof(EAX), 32, 0b000, nameof(AL), nameof(AH), nameof(AX));
    // End Accumulator

    // Base index
    public static readonly x86Register BL = new(nameof(BL), 8, 0b011);
    public static readonly x86Register BH = new(nameof(BH), 8, 0b111);
    public static readonly x86Register BX = new(nameof(BX), 16, 0b011, nameof(BL), nameof(BH));

    public static readonly x86Register EBX = new(nameof(EBX), 32, 0b011, nameof(BL), nameof(BH), nameof(BX));
    // End Base index

    // Counter
    public static readonly x86Register CL = new(nameof(CL), 8, 0b001);
    public static readonly x86Register CH = new(nameof(CH), 8, 0b101);
    public static readonly x86Register CX = new(nameof(CX), 16, 0b001, nameof(CL), nameof(CH));

    public static readonly x86Register ECX = new(nameof(ECX), 32, 0b001, nameof(CL), nameof(CH), nameof(CX));
    // End Counter

    // Extend accumulator
    public static readonly x86Register DL = new(nameof(DL), 8, 0b010);
    public static readonly x86Register DH = new(nameof(DH), 8, 0b110);
    public static readonly x86Register DX = new(nameof(DX), 16, 0b010, nameof(DL), nameof(DH));

    public static readonly x86Register EDX = new(nameof(EDX), 32, 0b010, nameof(DL), nameof(DH), nameof(DX));
    // End Extend accumulator

    // Stack pointer
    public static readonly x86Register SP = new(nameof(SP), 16, 0b100);

    public static readonly x86Register ESP = new(nameof(ESP), 32, 0b100, nameof(SP));
    // End Stack pointer

    // Stack base pointer
    public static readonly x86Register BP = new(nameof(BP), 16, 0b101);

    public static readonly x86Register EBP = new(nameof(EBP), 32, 0b101, nameof(BP));
    // End Stack base pointer

    // Source index
    public static readonly x86Register SI = new(nameof(SI), 16, 0b110);

    public static readonly x86Register ESI = new(nameof(ESI), 32, 0b110, nameof(SI));
    // End Source index

    // Destination index
    public static readonly x86Register DI = new(nameof(DI), 16, 0b111);

    public static readonly x86Register EDI = new(nameof(EDI), 32, 0b111, nameof(DI));
    // End Destination index

    // Instruction pointer
    public static readonly x86Register IP = new(nameof(IP), 16, 0b000);

    public static readonly x86Register EIP = new(nameof(EIP), 32, 0b000, nameof(IP));
    // End Instruction pointer

    // SIMD
    public static readonly x86Register MMX0 = new(nameof(MMX0), 64, 0b000);
    public static readonly x86Register MMX1 = new(nameof(MMX1), 64, 0b000);
    public static readonly x86Register MMX2 = new(nameof(MMX2), 64, 0b000);
    public static readonly x86Register MMX3 = new(nameof(MMX3), 64, 0b000);
    public static readonly x86Register MMX4 = new(nameof(MMX4), 64, 0b000);
    public static readonly x86Register MMX5 = new(nameof(MMX5), 64, 0b000);
    public static readonly x86Register MMX6 = new(nameof(MMX6), 64, 0b000);
    public static readonly x86Register MMX7 = new(nameof(MMX7), 64, 0b000);
    public static readonly x86Register MMX8 = new(nameof(MMX8), 64, 0b000);
    public static readonly x86Register MMX9 = new(nameof(MMX9), 64, 0b000);
    public static readonly x86Register MMX10 = new(nameof(MMX10), 64, 0b000);
    public static readonly x86Register MMX11 = new(nameof(MMX11), 64, 0b000);
    public static readonly x86Register MMX12 = new(nameof(MMX12), 64, 0b000);
    public static readonly x86Register MMX13 = new(nameof(MMX13), 64, 0b000);
    public static readonly x86Register MMX14 = new(nameof(MMX14), 64, 0b000);

    public static readonly x86Register MMX15 = new(nameof(MMX15), 64, 0b000);
    // End SIMD

    // SIMD
    public static readonly x86Register XMM0 = new(nameof(XMM0), 128, 0b000);
    public static readonly x86Register XMM1 = new(nameof(XMM1), 128, 0b000);
    public static readonly x86Register XMM2 = new(nameof(XMM2), 128, 0b000);
    public static readonly x86Register XMM3 = new(nameof(XMM3), 128, 0b000);
    public static readonly x86Register XMM4 = new(nameof(XMM4), 128, 0b000);
    public static readonly x86Register XMM5 = new(nameof(XMM5), 128, 0b000);
    public static readonly x86Register XMM6 = new(nameof(XMM6), 128, 0b000);
    public static readonly x86Register XMM7 = new(nameof(XMM7), 128, 0b000);
    public static readonly x86Register XMM8 = new(nameof(XMM8), 128, 0b000);
    public static readonly x86Register XMM9 = new(nameof(XMM9), 128, 0b000);
    public static readonly x86Register XMM10 = new(nameof(XMM10), 128, 0b000);
    public static readonly x86Register XMM11 = new(nameof(XMM11), 128, 0b000);
    public static readonly x86Register XMM12 = new(nameof(XMM12), 128, 0b000);
    public static readonly x86Register XMM13 = new(nameof(XMM13), 128, 0b000);
    public static readonly x86Register XMM14 = new(nameof(XMM14), 128, 0b000);

    public static readonly x86Register XMM15 = new(nameof(XMM15), 128, 0b000);
    // End SIMD

    // SIMD
    public static readonly x86Register YMM0 = new(nameof(YMM0), 256, 0b000, nameof(XMM0));
    public static readonly x86Register YMM1 = new(nameof(YMM1), 256, 0b000, nameof(XMM1));
    public static readonly x86Register YMM2 = new(nameof(YMM2), 256, 0b000, nameof(XMM2));
    public static readonly x86Register YMM3 = new(nameof(YMM3), 256, 0b000, nameof(XMM3));
    public static readonly x86Register YMM4 = new(nameof(YMM4), 256, 0b000, nameof(XMM4));
    public static readonly x86Register YMM5 = new(nameof(YMM5), 256, 0b000, nameof(XMM5));
    public static readonly x86Register YMM6 = new(nameof(YMM6), 256, 0b000, nameof(XMM6));
    public static readonly x86Register YMM7 = new(nameof(YMM7), 256, 0b000, nameof(XMM7));
    public static readonly x86Register YMM8 = new(nameof(YMM8), 256, 0b000, nameof(XMM8));
    public static readonly x86Register YMM9 = new(nameof(YMM9), 256, 0b000, nameof(XMM9));
    public static readonly x86Register YMM10 = new(nameof(YMM10), 256, 0b000, nameof(XMM10));
    public static readonly x86Register YMM11 = new(nameof(YMM11), 256, 0b000, nameof(XMM11));
    public static readonly x86Register YMM12 = new(nameof(YMM12), 256, 0b000, nameof(XMM12));
    public static readonly x86Register YMM13 = new(nameof(YMM13), 256, 0b000, nameof(XMM13));
    public static readonly x86Register YMM14 = new(nameof(YMM14), 256, 0b000, nameof(XMM14));
    public static readonly x86Register YMM15 = new(nameof(YMM15), 256, 0b000, nameof(XMM15));

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
    // End SIMD
}
