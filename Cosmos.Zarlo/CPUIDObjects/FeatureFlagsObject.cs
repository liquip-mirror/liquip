using System.Text;

namespace Cosmos.Zarlo.CPUIDObjects;

public class FeatureFlagsObject
{
    public int eax { get; private set; }
    public int ebx { get; private set; }
    public int ecx { get; private set; }
    public int edx { get; private set; }

    public FeatureFlagsObject()
    {
        int eax = 0;
        int ebx = 0;
        int ecx = 0;
        int edx = 0;

        CPUID.Raw(7, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;

        AVX512VL = CPUID.HasFlag(ebx, 31);
        AVX512BW = CPUID.HasFlag(ebx, 30);
        SHA = CPUID.HasFlag(ebx, 29);
        AVX512CD = CPUID.HasFlag(ebx, 28);
        AVX512ER = CPUID.HasFlag(ebx, 27);
        AVX512PF = CPUID.HasFlag(ebx, 26);
        PT = CPUID.HasFlag(ebx, 25);
        CLWB = CPUID.HasFlag(ebx, 24);
        CLFLUSHOPT = CPUID.HasFlag(ebx, 23);
        PCOMMIT = CPUID.HasFlag(ebx, 22);
        AVX512IFMA = CPUID.HasFlag(ebx, 21);
        SMAP = CPUID.HasFlag(ebx, 20);
        ADX = CPUID.HasFlag(ebx, 19);
        RDSEED = CPUID.HasFlag(ebx, 18);
        AVX512DQ = CPUID.HasFlag(ebx, 17);
        AVX512F = CPUID.HasFlag(ebx, 16);
        PQE = CPUID.HasFlag(ebx, 15);
        MPX = CPUID.HasFlag(ebx, 14);
        FPCSDS = CPUID.HasFlag(ebx, 13);
        PQM = CPUID.HasFlag(ebx, 12);
        RTM = CPUID.HasFlag(ebx, 11);
        INVPCID = CPUID.HasFlag(ebx, 10);
        ERMS = CPUID.HasFlag(ebx, 9);
        BMI2 = CPUID.HasFlag(ebx, 8);
        SMEP = CPUID.HasFlag(ebx, 7);
        FPDP = CPUID.HasFlag(ebx, 6);
        AVX2 = CPUID.HasFlag(ebx, 5);
        HLE = CPUID.HasFlag(ebx, 4);
        BMI1 = CPUID.HasFlag(ebx, 3);
        SGX = CPUID.HasFlag(ebx, 2);
        TSC_ADJUST = CPUID.HasFlag(ebx, 1);
        FSGSBASE = CPUID.HasFlag(ebx, 0);
    }

    //ebx
    public bool AVX512VL { get; init; }
    public bool AVX512BW { get; init; }
    public bool SHA { get; init; }
    public bool AVX512CD { get; init; }
    public bool AVX512ER { get; init; }
    public bool AVX512PF { get; init; }
    public bool PT { get; init; }
    public bool CLWB { get; init; }
    public bool CLFLUSHOPT { get; init; }
    public bool PCOMMIT { get; init; }
    public bool AVX512IFMA { get; init; }
    public bool SMAP { get; init; }
    public bool ADX { get; init; }
    public bool RDSEED { get; init; }
    public bool AVX512DQ { get; init; }
    public bool AVX512F { get; init; }
    public bool PQE { get; init; }
    public bool MPX { get; init; }
    public bool FPCSDS { get; init; }
    public bool PQM { get; init; }
    public bool RTM { get; init; }
    public bool INVPCID { get; init; }
    public bool ERMS { get; init; }
    public bool BMI2 { get; init; }
    public bool SMEP { get; init; }
    public bool FPDP { get; init; }
    public bool AVX2 { get; init; }
    public bool HLE { get; init; }
    public bool BMI1 { get; init; }
    public bool SGX { get; init; }
    public bool TSC_ADJUST { get; init; }
    public bool FSGSBASE { get; init; }


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