using System.Text;
using Cosmos.Core;

namespace Cosmos.Zarlo.CPUIDObjects;

public class ProcessorInfoObject
{
    public int eax { get; private set; } = 0;
    public int ebx { get; private set; } = 0;
    public int ecx { get; private set; } = 0;
    public int edx { get; private set; } = 0;

    public ProcessorInfoObject()
    {
        int eax = 0;
        int ebx = 0;
        int ecx = 0;
        int edx = 0;

        CPU.ReadCPUID(1, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;
        // SteppingID = (byte)CPUID.GetBitRange(eax, 0, 3);
        // Model      = (byte)CPUID.GetBitRange(eax, 4, 7);
        // FamilyID   = (byte)CPUID.GetBitRange(eax, 8, 11);
        // ProcessorType    = (byte)CPUID.GetBitRange(eax, 12, 13);
        // ExtendedModelID  = (byte)CPUID.GetBitRange(eax, 16, 19);
        // ExtendedFamilyID = (byte)CPUID.GetBitRange(eax, 20, 27);
        //
        // BrandID  = (byte)CPUID.GetBitRange(ebx, 0, 7);
        // CLFLUSH  = (byte)CPUID.GetBitRange(ebx, 8, 15);
        // CPUcount = (byte)CPUID.GetBitRange(ebx, 16, 23);
        // APICID   = (byte)CPUID.GetBitRange(ebx, 24, 31);
        //
        //
        // SSE3    = CPUID.HasFlag(ecx, 0);
        // PCLMUL  = CPUID.HasFlag(ecx, 1);
        // DTES64  = CPUID.HasFlag(ecx, 2);
        // MON     = CPUID.HasFlag(ecx, 3);
        // DSCPL   = CPUID.HasFlag(ecx, 4);
        // VMX     = CPUID.HasFlag(ecx, 5);
        // SMX     = CPUID.HasFlag(ecx, 6);
        // EST     = CPUID.HasFlag(ecx, 7);
        // TM2     = CPUID.HasFlag(ecx, 8);
        // SSSE3   = CPUID.HasFlag(ecx, 9);
        // CID     = CPUID.HasFlag(ecx, 10);
        // SDBG    = CPUID.HasFlag(ecx, 11);
        // FMA     = CPUID.HasFlag(ecx, 12);
        // CX16    = CPUID.HasFlag(ecx, 13);
        // ETPRD   = CPUID.HasFlag(ecx, 14);
        // PDCM    = CPUID.HasFlag(ecx, 15);
        // PCID    = CPUID.HasFlag(ecx, 17);
        // DCA     = CPUID.HasFlag(ecx, 18);
        // SSE4_1  = CPUID.HasFlag(ecx, 19);
        // SSE4_2  = CPUID.HasFlag(ecx, 20);
        // x2APIC  = CPUID.HasFlag(ecx, 21);
        // MOVBE   = CPUID.HasFlag(ecx, 22);
        // POPCNT  = CPUID.HasFlag(ecx, 23);
        // TSCD    = CPUID.HasFlag(ecx, 24);
        // AES     = CPUID.HasFlag(ecx, 25);
        // XSAVE   = CPUID.HasFlag(ecx, 26);
        // OSXSAVE = CPUID.HasFlag(ecx, 27);
        // AVX     = CPUID.HasFlag(ecx, 28);
        // F16C    = CPUID.HasFlag(ecx, 29);
        // RDRAND  = CPUID.HasFlag(ecx, 30);
        // HV      = CPUID.HasFlag(ecx, 31);
        //
        // FPU   = CPUID.HasFlag(edx, 0);
        // VME   = CPUID.HasFlag(edx, 1);
        // DE    = CPUID.HasFlag(edx, 2);
        // PSE   = CPUID.HasFlag(edx, 3);
        // TSC   = CPUID.HasFlag(edx, 4);
        // MSR   = CPUID.HasFlag(edx, 5);
        // PAE   = CPUID.HasFlag(edx, 6);
        // MCE   = CPUID.HasFlag(edx, 7);
        // CX8   = CPUID.HasFlag(edx, 8);
        // APIC  = CPUID.HasFlag(edx, 9);
        // SEP   = CPUID.HasFlag(edx, 11);
        // MTRR  = CPUID.HasFlag(edx, 12);
        // PGE   = CPUID.HasFlag(edx, 13);
        // MCA   = CPUID.HasFlag(edx, 14);
        // CMOV  = CPUID.HasFlag(edx, 15);
        // PAT   = CPUID.HasFlag(edx, 16);
        // PSE36 = CPUID.HasFlag(edx, 17);
        // PSN   = CPUID.HasFlag(edx, 18);
        // CLFL  = CPUID.HasFlag(edx, 19);
        // DTES  = CPUID.HasFlag(edx, 21);
        // ACPI  = CPUID.HasFlag(edx, 22);
        // MMX   = CPUID.HasFlag(edx, 23);
        // FXSR  = CPUID.HasFlag(edx, 24);
        // SSE   = CPUID.HasFlag(edx, 25);
        // SSE2  = CPUID.HasFlag(edx, 26);
        // SS    = CPUID.HasFlag(edx, 27);
        // HTT   = CPUID.HasFlag(edx, 28);
        // TM1   = CPUID.HasFlag(edx, 29);
        // IA_64 = CPUID.HasFlag(edx, 30);
        // PBE   = CPUID.HasFlag(edx, 31);
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
        sb.Append(eax);
        sb.Append(" ebx0: ");
        sb.Append(ebx);
        sb.Append(" ecx0: ");
        sb.Append(ecx);
        sb.Append(" edx0: ");
        sb.Append(edx);
        return sb.ToString();
    }
}