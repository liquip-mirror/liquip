using System.Text;
using Cosmos.Core;

namespace Cosmos.Zarlo.CPUIDObjects;

public class PowerManagementInformationObject
{
    public int eax { get; private set; }
    public int ebx { get; private set; }
    public int ecx { get; private set; }
    public int edx { get; private set; }

    public PowerManagementInformationObject()
    {
        int eax = 0;
        int ebx = 0;
        int ecx = 0;
        int edx = 0;

        CPU.ReadCPUID(6, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;

        DTS = CPUID.HasFlag(eax, 0);
        DA = CPUID.HasFlag(eax, 1);
        ARAT = CPUID.HasFlag(eax, 2);
        PLN = CPUID.HasFlag(eax, 4);
        ECMD = CPUID.HasFlag(eax, 5);
        PTM = CPUID.HasFlag(eax, 6);
        HWP = CPUID.HasFlag(eax, 7);
        HWP_NOT = CPUID.HasFlag(eax, 8);
        HWP_ACT = CPUID.HasFlag(eax, 9);
        HWP_EPP = CPUID.HasFlag(eax, 10);
        HWP_PLR = CPUID.HasFlag(eax, 11);
        HDC = CPUID.HasFlag(eax, 13);

        ProgrammableDigitalThermalSensorInterruptThresholds = (byte)CPUID.GetBitRange(ebx, 0, 3);
    }

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