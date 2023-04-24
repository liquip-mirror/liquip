// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Runtime.CompilerServices;
using Cosmos.Core;
using Cosmos.Zarlo.CPUIDObjects;
using IL2CPU.API;
using XSharp;
using XSharp.Zarlo;
using XSharp.Zarlo.Fluent;
using static XSharp.XSRegisters;

namespace Cosmos.Zarlo;

// https://sandpile.org/x86/cpuid.htm

public class CPUID
{
    [Inline]
    public static void Raw(uint type, uint subType, ref int eax, ref int ebx, ref int ecx, ref int edx)
    {
        var args = ArgumentBuilder.Inline();
        FluentXSharp.NewX86()
            .SetPointer(EAX, args.GetArg(nameof(type)))
            .SetPointer(ECX, args.GetArg(nameof(subType)))
            .Cpuid()
            .SetPointer(EDI, args.GetArg(nameof(eax)))
            .Set(EDI, EAX, destinationIsIndirect: true)
            .SetPointer(EDI, args.GetArg(nameof(ebx)))
            .Set(EDI, EBX, destinationIsIndirect: true)
            .SetPointer(EDI, args.GetArg(nameof(ecx)))
            .Set(EDI, ECX, destinationIsIndirect: true)
            .SetPointer(EDI, args.GetArg(nameof(edx)))
            .Set(EDI, EDX, destinationIsIndirect: true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Raw(uint type, ref int eax, ref int ebx, ref int ecx, ref int edx)
    {
        CPU.ReadCPUID(type, ref eax, ref ebx, ref ecx, ref edx);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlag(Int32 data, int flagOffset) => HasFlag((UInt32)data, flagOffset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlag(UInt32 data, int flagOffset)
    {
        if (flagOffset is < 0 or > 31) throw new ArgumentOutOfRangeException("flagOffset");
        var flag = data >> flagOffset;
        return (flag & 1) == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32 GetBitRange(Int32 data, int start, int end)
    {
        return (Int32)GetBitRange((UInt32)data, start, end);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt32 GetBitRange(UInt32 data, int start, int end)
    {
        //shift binary to starting point of range
        UInt32 shifted = (data >> start);

        //calculate range length (+1 for 0 index)
        int rangeLength = (end - start) + 1;

        //get binary mask based on range length 

        UInt32 maskBinary = rangeLength switch
        {
            1 => 0b00000000_00000000_00000000_00000001,
            2 => 0b00000000_00000000_00000000_00000011,
            3 => 0b00000000_00000000_00000000_00000111,
            4 => 0b00000000_00000000_00000000_00001111,
            5 => 0b00000000_00000000_00000000_00011111,
            6 => 0b00000000_00000000_00000000_00111111,
            7 => 0b00000000_00000000_00000000_01111111,
            8 => 0b00000000_00000000_00000000_11111111,
            9 => 0b00000000_00000000_00000001_11111111,
            10 => 0b00000000_00000000_00000011_11111111,
            11 => 0b00000000_00000000_00000111_11111111,
            12 => 0b00000000_00000000_00001111_11111111,
            13 => 0b00000000_00000000_00011111_11111111,
            14 => 0b00000000_00000000_00111111_11111111,
            15 => 0b00000000_00000000_01111111_11111111,
            16 => 0b00000000_00000000_11111111_11111111,
            17 => 0b00000000_00000001_11111111_11111111,
            18 => 0b00000000_00000011_11111111_11111111,
            19 => 0b00000000_00000111_11111111_11111111,
            20 => 0b00000000_00001111_11111111_11111111,
            21 => 0b00000000_00011111_11111111_11111111,
            22 => 0b00000000_00111111_11111111_11111111,
            23 => 0b00000000_01111111_11111111_11111111,
            24 => 0b00000000_11111111_11111111_11111111,
            25 => 0b00000001_11111111_11111111_11111111,
            26 => 0b00000011_11111111_11111111_11111111,
            27 => 0b00000111_11111111_11111111_11111111,
            28 => 0b00001111_11111111_11111111_11111111,
            29 => 0b00011111_11111111_11111111_11111111,
            30 => 0b00111111_11111111_11111111_11111111,
            31 => 0b01111111_11111111_11111111_11111111,
            32 => 0b11111111_11111111_11111111_11111111,
            _ => throw new ArgumentOutOfRangeException($@"end {rangeLength}")
        };

        return (shifted & maskBinary);
    }

    static CPUID()
    {
        ProcessorInfo = new ProcessorInfoObject();
        CacheConfiguration = new CacheConfigurationObject();
        PowerManagementInformation = new PowerManagementInformationObject();
        ProcessorFrequencyInformation = new ProcessorFrequencyInformationObject();
        FeatureFlags = new FeatureFlagsObject();
    }

    public static readonly ProcessorInfoObject ProcessorInfo;

    public static readonly CacheConfigurationObject CacheConfiguration;

    public static readonly PowerManagementInformationObject PowerManagementInformation;

    public static readonly ProcessorFrequencyInformationObject ProcessorFrequencyInformation;

    public static readonly FeatureFlagsObject FeatureFlags;
}