using System;
using System.Text;
using Cosmos.Core;

namespace Zarlo.Cosmos.CPUIDObjects;

public class ProcessorInfoObject
{
    public int eax { get; private set; } = 0;
    public int ebx { get; private set; } = 0;
    public int ecx { get; private set; } = 0;
    public int edx { get; private set; } = 0;

    public ProcessorInfoObject()
    {
        Console.WriteLine("LOADING ProcessorInfoObject");
        int eax = 0;
        int ebx = 0;
        int ecx = 0;
        int edx = 0;

        CPUID.Raw(1, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;
        Console.WriteLine("eax");
        SteppingID = (byte)CPUID.GetBitRange(eax, 0, 3);
        Model      = (byte)CPUID.GetBitRange(eax, 4, 7);
        FamilyID   = (byte)CPUID.GetBitRange(eax, 8, 11);
        ProcessorType    = (byte)CPUID.GetBitRange(eax, 12, 13);
        ExtendedModelID  = (byte)CPUID.GetBitRange(eax, 16, 19);
        ExtendedFamilyID = (byte)CPUID.GetBitRange(eax, 20, 27);
        
        Console.WriteLine("ebx");
        BrandID  = (byte)CPUID.GetBitRange(ebx, 0, 7);
        CLFLUSH  = (byte)CPUID.GetBitRange(ebx, 8, 15);
        CPUcount = (byte)CPUID.GetBitRange(ebx, 16, 23);
        APICID   = (byte)CPUID.GetBitRange(ebx, 24, 31);
        
        Console.WriteLine("ecx");
        SSE3    = CPUID.HasFlag(ref ecx, 0);
        PCLMUL  = CPUID.HasFlag(ref ecx, 1);
        DTES64  = CPUID.HasFlag(ref ecx, 2);
        MON     = CPUID.HasFlag(ref ecx, 3);
        DSCPL   = CPUID.HasFlag(ref ecx, 4);
        VMX     = CPUID.HasFlag(ref ecx, 5);
        SMX     = CPUID.HasFlag(ref ecx, 6);
        EST     = CPUID.HasFlag(ref ecx, 7);
        TM2     = CPUID.HasFlag(ref ecx, 8);
        SSSE3   = CPUID.HasFlag(ref ecx, 9);
        CID     = CPUID.HasFlag(ref ecx, 10);
        SDBG    = CPUID.HasFlag(ref ecx, 11);
        FMA     = CPUID.HasFlag(ref ecx, 12);
        CX16    = CPUID.HasFlag(ref ecx, 13);
        ETPRD   = CPUID.HasFlag(ref ecx, 14);
        PDCM    = CPUID.HasFlag(ref ecx, 15);
        PCID    = CPUID.HasFlag(ref ecx, 17);
        DCA     = CPUID.HasFlag(ref ecx, 18);
        SSE4_1  = CPUID.HasFlag(ref ecx, 19);
        SSE4_2  = CPUID.HasFlag(ref ecx, 20);
        x2APIC  = CPUID.HasFlag(ref ecx, 21);
        MOVBE   = CPUID.HasFlag(ref ecx, 22);
        POPCNT  = CPUID.HasFlag(ref ecx, 23);
        TSCD    = CPUID.HasFlag(ref ecx, 24);
        AES     = CPUID.HasFlag(ref ecx, 25);
        XSAVE   = CPUID.HasFlag(ref ecx, 26);
        OSXSAVE = CPUID.HasFlag(ref ecx, 27);
        AVX     = CPUID.HasFlag(ref ecx, 28);
        F16C    = CPUID.HasFlag(ref ecx, 29);
        RDRAND  = CPUID.HasFlag(ref ecx, 30);
        HV      = CPUID.HasFlag(ref ecx, 31);
        
        Console.WriteLine("edx");
        FPU   = CPUID.HasFlag(ref edx, 0);
        VME   = CPUID.HasFlag(ref edx, 1);
        DE    = CPUID.HasFlag(ref edx, 2);
        PSE   = CPUID.HasFlag(ref edx, 3);
        TSC   = CPUID.HasFlag(ref edx, 4);
        MSR   = CPUID.HasFlag(ref edx, 5);
        PAE   = CPUID.HasFlag(ref edx, 6);
        MCE   = CPUID.HasFlag(ref edx, 7);
        CX8   = CPUID.HasFlag(ref edx, 8);
        APIC  = CPUID.HasFlag(ref edx, 9);
        SEP   = CPUID.HasFlag(ref edx, 11);
        MTRR  = CPUID.HasFlag(ref edx, 12);
        PGE   = CPUID.HasFlag(ref edx, 13);
        MCA   = CPUID.HasFlag(ref edx, 14);
        CMOV  = CPUID.HasFlag(ref edx, 15);
        PAT   = CPUID.HasFlag(ref edx, 16);
        PSE36 = CPUID.HasFlag(ref edx, 17);
        PSN   = CPUID.HasFlag(ref edx, 18);
        CLFL  = CPUID.HasFlag(ref edx, 19);
        DTES  = CPUID.HasFlag(ref edx, 21);
        ACPI  = CPUID.HasFlag(ref edx, 22);
        MMX   = CPUID.HasFlag(ref edx, 23);
        FXSR  = CPUID.HasFlag(ref edx, 24);
        SSE   = CPUID.HasFlag(ref edx, 25);
        SSE2  = CPUID.HasFlag(ref edx, 26);
        SS    = CPUID.HasFlag(ref edx, 27);
        HTT   = CPUID.HasFlag(ref edx, 28);
        TM1   = CPUID.HasFlag(ref edx, 29);
        IA_64 = CPUID.HasFlag(ref edx, 30);
        PBE   = CPUID.HasFlag(ref edx, 31);

        Console.WriteLine("DONE ProcessorInfoObject");

    }

    // eax
    public byte SteppingID { get; init; }
    public byte Model { get; init; }
    public byte FamilyID { get; init; }
    public byte ProcessorType { get; init; }
    public byte ExtendedModelID { get; init; }
    public byte ExtendedFamilyID { get; init; }

    // ebx
    public byte BrandID { get; init; }
    public byte CLFLUSH { get; init; }
    public byte CPUcount { get; init; }
    public byte APICID { get; init; }

    // ecx
    public bool SSE3 { get; init; }
    public bool PCLMUL { get; init; }
    public bool DTES64 { get; init; }
    public bool MON { get; init; }
    public bool DSCPL { get; init; }
    public bool VMX { get; init; }
    public bool SMX { get; init; }
    public bool EST { get; init; }
    public bool TM2 { get; init; }
    public bool SSSE3 { get; init; }
    public bool CID { get; init; }
    public bool SDBG { get; init; }
    public bool FMA { get; init; }
    public bool CX16 { get; init; }
    public bool ETPRD { get; init; }
    public bool PDCM { get; init; }
    public bool PCID { get; init; }
    public bool DCA { get; init; }
    public bool SSE4_1 { get; init; }
    public bool SSE4_2 { get; init; }
    public bool x2APIC { get; init; }
    public bool MOVBE { get; init; }
    public bool POPCNT { get; init; }
    public bool TSCD { get; init; }
    public bool AES { get; init; }
    public bool XSAVE { get; init; }
    public bool OSXSAVE { get; init; }
    public bool AVX { get; init; }
    public bool F16C { get; init; }
    public bool RDRAND { get; init; }
    public bool HV { get; init; }

    // edx
    public bool FPU { get; init; }
    public bool VME { get; init; }
    public bool DE { get; init; }
    public bool PSE { get; init; }
    public bool TSC { get; init; }
    public bool MSR { get; init; }
    public bool PAE { get; init; }
    public bool MCE { get; init; }
    public bool CX8 { get; init; }
    public bool APIC { get; init; }
    public bool SEP { get; init; }
    public bool MTRR { get; init; }
    public bool PGE { get; init; }
    public bool MCA { get; init; }
    public bool CMOV { get; init; }
    public bool PAT { get; init; }
    public bool PSE36 { get; init; }
    public bool PSN { get; init; }
    public bool CLFL { get; init; }
    public bool DTES { get; init; }
    public bool ACPI { get; init; }
    public bool MMX { get; init; }
    public bool FXSR { get; init; }
    public bool SSE { get; init; }
    public bool SSE2 { get; init; }
    public bool SS { get; init; }
    public bool HTT { get; init; }
    public bool TM1 { get; init; }
    public bool IA_64 { get; init; }
    public bool PBE { get; init; }

    public string DebugString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("eax0: ");
        sb.Append($@"{eax}");
        sb.AppendLine();
        sb.Append("ebx0: ");
        sb.Append($@"{ebx}");
        sb.AppendLine();
        // Console.WriteLine("2");
        // sb.Append("ecx0: ");
        // sb.Append($@"{ecx}");
        // sb.AppendLine();
        sb.Append("edx0: ");
        sb.Append($@"{edx}");
        return sb.ToString();
    }
}
