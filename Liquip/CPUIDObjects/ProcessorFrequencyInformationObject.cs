using System.Text;

namespace Liquip.CPUIDObjects;

public class ProcessorFrequencyInformationObject
{
    public ProcessorFrequencyInformationObject()
    {
        var eax0 = 0;
        var ebx0 = 0;
        var ecx0 = 0;
        var edx0 = 0;

        CPUID.Raw(21, ref eax0, ref ebx0, ref ecx0, ref edx0);

        this.eax0 = eax0;
        this.ebx0 = ebx0;
        this.ecx0 = ecx0;
        this.edx0 = edx0;

        Denominator = eax0;
        Numerator = ebx0;
        CoreCrystalClockFrequency = ecx0;
        var eax1 = 0;
        var ebx1 = 0;
        var ecx1 = 0;
        var edx1 = 0;

        CPUID.Raw(22, ref eax1, ref ebx1, ref ecx1, ref edx1);

        this.eax1 = eax1;
        this.ebx1 = ebx1;
        this.ecx1 = ecx1;
        this.edx1 = edx1;

        CoreBaseFrequency = (short)CPUID.GetBitRange(eax1, 0, 15);
        CoreMaximumFrequency = (short)CPUID.GetBitRange(ebx1, 0, 15);
        BusFrequency = (short)CPUID.GetBitRange(ecx1, 0, 15);
    }

    public int eax0 { get; }
    public int ebx0 { get; }
    public int ecx0 { get; }
    public int edx0 { get; }

    public int eax1 { get; }
    public int ebx1 { get; }
    public int ecx1 { get; }
    public int edx1 { get; }

    // 0

    public int Denominator { get; init; }
    public int Numerator { get; init; }
    public int CoreCrystalClockFrequency { get; init; }

    // 1

    public short CoreBaseFrequency { get; init; }
    public short CoreMaximumFrequency { get; init; }
    public short BusFrequency { get; init; }

    public string DebugString()
    {
        var sb = new StringBuilder();
        sb.Append("eax0: ");
        sb.Append(eax0);
        sb.Append("ebx0: ");
        sb.Append(ebx0);
        sb.Append("ecx0: ");
        sb.Append(ecx0);
        sb.Append("edx0: ");
        sb.Append(edx0);

        sb.Append("eax1: ");
        sb.Append(eax1);
        sb.Append("ebx1: ");
        sb.Append(ebx1);
        sb.Append("ecx1: ");
        sb.Append(ecx1);
        sb.Append("edx1: ");
        sb.Append(edx1);

        return sb.ToString();
    }
}
