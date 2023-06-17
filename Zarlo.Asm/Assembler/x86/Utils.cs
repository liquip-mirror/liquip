namespace Zarlo.Asm.Assembler.x86;

public enum Mod : byte
{ 
    A = 0b00_000_000,
    B = 0b10_000_000,
    C = 0b01_000_000,
    D = 0b11_000_000,
}

public enum Reg : byte
{ 
    A = 0b00_000_000,
    B = 0b00_100_000,
    C = 0b00_010_000,
    D = 0b00_110_000,
    E = 0b00_001_000,
    F = 0b00_101_000,
    G = 0b00_011_000,
    H = 0b00_111_000,

}

public enum Rm : byte
{ 
    A = 0b00_000_000,
    B = 0b00_000_100,
    C = 0b00_000_010,
    D = 0b00_000_110,
    E = 0b00_000_001,
    F = 0b00_000_101,
    G = 0b00_000_011,
    H = 0b00_000_111,

}

public enum ScaleFactor
{ 
    Factor1 = 0b00,
    Factor2 = 0b01,
    Factor4 = 0b10,
    Factor8 = 0b11
}

public class Utils
{

    public static byte ModR_M(Mod mod, Reg reg, Rm rm) => (byte)((byte)mod | (byte)reg | (byte)rm);

    public static byte ModR_M(Mod mod, x86Register reg, Rm rm) 
    { 
        return (byte)((byte)mod | reg.Code(2) | (byte)rm);
    }


    public static byte SIB(ScaleFactor scale, Reg index, Reg _base) => (byte)((byte)scale | (byte)index | (byte)_base);

    public static byte SIB(ScaleFactor scale, x86Register index, x86Register _base) 
    { 
        return (byte)((byte)scale | index.Code(2) | index.Code(5));
    }

}
