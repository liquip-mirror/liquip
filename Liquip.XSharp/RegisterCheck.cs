using static XSharp.XSRegisters;

namespace Liquip.XSharp;

public static partial class Registers
{
    public static void CheckX86(params Register[] registers)
    {
        foreach (var item in RegisterCheckX86.AllGroups)
        {
            if (item.Count(i => registers.Contains(i)) > 1)
            {
            }
        }
    }

    public static class RegisterCheckX86
    {
        public static readonly Register[][] AllGroups;

        public static readonly Register[] Accumulator;
        public static readonly Register[] Base;
        public static readonly Register[] Counter;
        public static readonly Register[] Data;


        static RegisterCheckX86()
        {
            Accumulator = new Register[] { EAX, AX, AH, AL };

            Base = new Register[] { EBX, BX, BH, BL };

            Counter = new Register[] { ECX, CX, CH, CL };

            Data = new Register[] { EDX, DX, DH, DL };

            AllGroups = new[] { Accumulator, Base, Counter, Data };
        }
    }
}
