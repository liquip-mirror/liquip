using Cosmos.Core;

using Cosmos.Zarlo.CPUIDObjects;

namespace Cosmos.Zarlo;

// https://sandpile.org/x86/cpuid.htm

public class CPUID
{

    
    public static bool HasFlag(Int32 data, int flagOffest) => HasFlag((UInt32)data, flagOffest);

    public static bool HasFlag(UInt32 data, int flagOffest)
    {
        if(flagOffest < 0 || flagOffest > 31) throw new ArgumentOutOfRangeException("flagOffest");
        var flag = data >> flagOffest;
        return (flag & 1) == 1;
    }

    public static Int32 GetBitRange(Int32 data, int start, int end)
    {
        return (Int32)GetBitRange((UInt32)data, start, end);
    }
    public static UInt32 GetBitRange(UInt32 data, int start, int end) {

        //shift binary to starting point of range
        UInt32 shifted = (data >> start);

        //calculate range length (+1 for 0 index)
        int rangeLength = (end-start)+1;

        //get binary mask based on range length 
        UInt32 maskBinary;
        
        switch (rangeLength){
            case 1: maskBinary = 0b00000000_00000000_00000000_00000001; break;
            case 2: maskBinary = 0b00000000_00000000_00000000_00000011; break;
            case 3: maskBinary = 0b00000000_00000000_00000000_00000111; break;
            case 4: maskBinary = 0b00000000_00000000_00000000_00001111; break;
            case 5: maskBinary = 0b00000000_00000000_00000000_00011111; break;
            case 6: maskBinary = 0b00000000_00000000_00000000_00111111; break;
            case 7: maskBinary = 0b00000000_00000000_00000000_01111111; break;
            case 8: maskBinary = 0b00000000_00000000_00000000_11111111; break;

            case 9:  maskBinary = 0b00000000_00000000_00000001_11111111; break;
            case 10: maskBinary = 0b00000000_00000000_00000011_11111111; break;
            case 11: maskBinary = 0b00000000_00000000_00000111_11111111; break;
            case 12: maskBinary = 0b00000000_00000000_00001111_11111111; break;
            case 13: maskBinary = 0b00000000_00000000_00011111_11111111; break;
            case 14: maskBinary = 0b00000000_00000000_00111111_11111111; break;
            case 15: maskBinary = 0b00000000_00000000_01111111_11111111; break;
            case 16: maskBinary = 0b00000000_00000000_11111111_11111111; break;

            case 17: maskBinary = 0b00000000_00000001_11111111_11111111; break;
            case 18: maskBinary = 0b00000000_00000011_11111111_11111111; break;
            case 19: maskBinary = 0b00000000_00000111_11111111_11111111; break;
            case 20: maskBinary = 0b00000000_00001111_11111111_11111111; break;
            case 21: maskBinary = 0b00000000_00011111_11111111_11111111; break;
            case 22: maskBinary = 0b00000000_00111111_11111111_11111111; break;
            case 23: maskBinary = 0b00000000_01111111_11111111_11111111; break;
            case 24: maskBinary = 0b00000000_11111111_11111111_11111111; break;

            case 25: maskBinary = 0b00000001_11111111_11111111_11111111; break;
            case 26: maskBinary = 0b00000011_11111111_11111111_11111111; break;
            case 27: maskBinary = 0b00000111_11111111_11111111_11111111; break;
            case 28: maskBinary = 0b00001111_11111111_11111111_11111111; break;
            case 29: maskBinary = 0b00011111_11111111_11111111_11111111; break;
            case 30: maskBinary = 0b00111111_11111111_11111111_11111111; break;
            case 31: maskBinary = 0b01111111_11111111_11111111_11111111; break;
            case 32: maskBinary = 0b11111111_11111111_11111111_11111111; break;

            default:
            throw new ArgumentOutOfRangeException($@"end {rangeLength}");
        }

        return (shifted & maskBinary);
    }

    public static ProcessorInfoObject ProcessorInfo = new ProcessorInfoObject();

    public static CacheConfigurationObject CacheConfiguration = new CacheConfigurationObject();

    public static PowerManagementInformationObject PowerManagementInformation = new PowerManagementInformationObject();

    public static ProcessorFrequencyInformationObject ProcessorFrequencyInformation = new ProcessorFrequencyInformationObject();

}
