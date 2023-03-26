using System.Text;
using Cosmos.Core;

namespace Cosmos.Zarlo.CPUIDObjects;

public class CacheConfigurationObject {

    public CacheConfigurationObject() {         

    }

    public CacheObject GetCache(int index) => new CacheObject(index);

    public CacheObject L1D = new CacheObject(0);
    public CacheObject L1I = new CacheObject(1);
    public CacheObject L2 = new CacheObject(2);

    public string DebugString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("L1D: ");
        sb.AppendLine(L1D.DebugString());
        sb.Append("L1I: ");
        sb.AppendLine(L1I.DebugString());
        sb.Append("L2: ");
        sb.Append(L2.DebugString());
        return sb.ToString();
    }
    
}

public class CacheObject {
    public int eax { get; private set; } = 0;
    public int ebx { get; private set; } = 0;
    public int ecx { get; private set; } = 0;
    public int edx { get; private set; } = 0;
    public CacheObject(int id)
    {
        int eax = 0;
        int ebx = 0;
        int ecx = id;
        int edx = 0;
        
        CPU.ReadCPUID(4, ref eax, ref ebx, ref ecx, ref edx);

        this.eax = eax;
        this.ebx = ebx;
        this.ecx = ecx;
        this.edx = edx;

        CacheType = (byte)CPUID.GetBitRange(eax, 0, 4);
        CacheLevel = (byte)CPUID.GetBitRange(eax, 5, 7);
        SelfInitializing = CPUID.HasFlag(eax, 8);
        FullyAssociative = CPUID.HasFlag(eax, 9);

        ThreadsPerCache = (short)CPUID.GetBitRange(eax, 14, 25);

        CoresPerPackage = (short)CPUID.GetBitRange(eax, 26, 31);

        SystemCoherencyLineSize = (short)CPUID.GetBitRange(ebx, 0, 11);
        PhysicalLinePartitions = (short)CPUID.GetBitRange(ebx, 12, 21);
        WaysOfAssociativity = (short)CPUID.GetBitRange(ebx, 22, 31);

        Sets = ecx;

        Write_BackInvalidate = CPUID.HasFlag(edx, 0);
        InclusiveOfLowerLevels = CPUID.HasFlag(edx, 1);
        ComplexIndexing = CPUID.HasFlag(edx, 2);
    }

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