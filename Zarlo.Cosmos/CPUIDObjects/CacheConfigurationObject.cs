using System.Text;

namespace Zarlo.Cosmos.CPUIDObjects;

public class CacheConfigurationObject
{
    public CacheObject L1D = new(0);
    public CacheObject L1I = new(1);
    public CacheObject L2 = new(2);

    public CacheObject GetCache(int index)
    {
        return new CacheObject(index);
    }

    public string DebugString()
    {
        var sb = new StringBuilder();
        sb.Append("L1D: ");
        sb.AppendLine(L1D.DebugString());
        sb.Append("L1I: ");
        sb.AppendLine(L1I.DebugString());
        sb.Append("L2: ");
        sb.Append(L2.DebugString());
        return sb.ToString();
    }
}

public class CacheObject
{
    public CacheObject(int id)
    {
        var eax = 0;
        var ebx = 0;
        var ecx = id;
        var edx = 0;

        CPUID.Raw(4, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;

        CacheType = (byte)CPUID.GetBitRange(eax, 0, 4);
        CacheLevel = (byte)CPUID.GetBitRange(eax, 5, 7);
        SelfInitializing = CPUID.HasFlag(ref eax, 8);
        FullyAssociative = CPUID.HasFlag(ref eax, 9);

        ThreadsPerCache = (short)CPUID.GetBitRange(eax, 14, 25);

        CoresPerPackage = (short)CPUID.GetBitRange(eax, 26, 31);

        SystemCoherencyLineSize = (short)CPUID.GetBitRange(ebx, 0, 11);
        PhysicalLinePartitions = (short)CPUID.GetBitRange(ebx, 12, 21);
        WaysOfAssociativity = (short)CPUID.GetBitRange(ebx, 22, 31);

        Sets = ecx;

        Write_BackInvalidate = CPUID.HasFlag(ref edx, 0);
        InclusiveOfLowerLevels = CPUID.HasFlag(ref edx, 1);
        ComplexIndexing = CPUID.HasFlag(ref edx, 2);
    }

    public int eax { get; }
    public int ebx { get; }
    public int ecx { get; }
    public int edx { get; }

    //eax
    public byte CacheType { get; init; }
    public byte CacheLevel { get; init; }

    public bool SelfInitializing { get; init; }
    public bool FullyAssociative { get; init; }

    public short ThreadsPerCache { get; init; }
    public short CoresPerPackage { get; init; }


    //ebx
    public short SystemCoherencyLineSize { get; init; }
    public short PhysicalLinePartitions { get; init; }
    public short WaysOfAssociativity { get; init; }

    //ecx
    public int Sets { get; init; }

    //edx
    public bool Write_BackInvalidate { get; init; }
    public bool InclusiveOfLowerLevels { get; init; }
    public bool ComplexIndexing { get; init; }

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
