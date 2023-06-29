using System.Text;

namespace Zarlo.Cosmos.CPUIDObjects;

public class PowerManagementInformationObject
{
    public PowerManagementInformationObject()
    {
        var eax = 0;
        var ebx = 0;
        var ecx = 0;
        var edx = 0;

        CPUID.Raw(6, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;

        DTS = CPUID.HasFlag(ref eax, 0);
        DA = CPUID.HasFlag(ref eax, 1);
        ARAT = CPUID.HasFlag(ref eax, 2);
        PLN = CPUID.HasFlag(ref eax, 4);
        ECMD = CPUID.HasFlag(ref eax, 5);
        PTM = CPUID.HasFlag(ref eax, 6);
        HWP = CPUID.HasFlag(ref eax, 7);
        HWP_NOT = CPUID.HasFlag(ref eax, 8);
        HWP_ACT = CPUID.HasFlag(ref eax, 9);
        HWP_EPP = CPUID.HasFlag(ref eax, 10);
        HWP_PLR = CPUID.HasFlag(ref eax, 11);
        HDC = CPUID.HasFlag(ref eax, 13);

        ProgrammableDigitalThermalSensorInterruptThresholds = (byte)CPUID.GetBitRange(ebx, 0, 3);
    }

    public int eax { get; }
    public int ebx { get; }
    public int ecx { get; }
    public int edx { get; }

    //eax
    public bool DTS { get; init; }
    public bool DA { get; init; }
    public bool ARAT { get; init; }
    public bool PLN { get; init; }
    public bool ECMD { get; init; }
    public bool PTM { get; init; }
    public bool HWP { get; init; }
    public bool HWP_NOT { get; init; }
    public bool HWP_ACT { get; init; }
    public bool HWP_EPP { get; init; }
    public bool HWP_PLR { get; init; }
    public bool HDC { get; init; }


    //ebx
    public byte ProgrammableDigitalThermalSensorInterruptThresholds { get; init; }

    public string DebugString()
    {
        var sb = new StringBuilder();
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
