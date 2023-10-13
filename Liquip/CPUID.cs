// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Cosmos.Core;
using Liquip.CPUIDObjects;
using Liquip.Utils;
using Liquip.XSharp;

namespace Liquip;

// https://sandpile.org/x86/cpuid.htm

/// <summary>
/// a wrapper around CpuId
/// </summary>
public class CPUID
{
    /// <summary>
    /// ProcessorInfo
    /// </summary>
    public static readonly ProcessorInfoObject ProcessorInfo;

    /// <summary>
    /// CacheConfiguration
    /// </summary>
    public static readonly CacheConfigurationObject CacheConfiguration;

    /// <summary>
    /// PowerManagementInformation
    /// </summary>
    public static readonly PowerManagementInformationObject PowerManagementInformation;

    /// <summary>
    /// ProcessorFrequencyInformation
    /// </summary>
    public static readonly ProcessorFrequencyInformationObject ProcessorFrequencyInformation;

    /// <summary>
    /// FeatureFlags
    /// </summary>
    public static readonly FeatureFlagsObject FeatureFlags;

    /// <summary>
    /// load data in the the objects
    /// </summary>
    static CPUID()
    {
        ProcessorInfo = new ProcessorInfoObject();
        CacheConfiguration = new CacheConfigurationObject();
        PowerManagementInformation = new PowerManagementInformationObject();
        ProcessorFrequencyInformation = new ProcessorFrequencyInformationObject();
        FeatureFlags = new FeatureFlagsObject();
    }

    /// <summary>
    /// a raw call to CpuId
    /// </summary>
    /// <param name="type"></param>
    /// <param name="subType"></param>
    /// <param name="eax"></param>
    /// <param name="ebx"></param>
    /// <param name="ecx"></param>
    /// <param name="edx"></param>
    /// <exception cref="ImplementedInPlugException"></exception>
    public static void Raw(uint type, uint subType, ref int eax, ref int ebx, ref int ecx, ref int edx)
    {
        (eax, ebx, ecx, edx) = X86Base.CpuId((int)type, (int)subType);
    }

    /// <summary>
    /// a raw call to CpuId
    /// </summary>
    /// <param name="type"></param>
    /// <param name="eax"></param>
    /// <param name="ebx"></param>
    /// <param name="ecx"></param>
    /// <param name="edx"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Raw(uint type, ref int eax, ref int ebx, ref int ecx, ref int edx)
    {
        (eax, ebx, ecx, edx) = X86Base.CpuId((int)type, 0);
    }

    /// <summary>
    /// gets if a bit is set
    /// </summary>
    /// <param name="data"></param>
    /// <param name="flagOffset"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlag(ref int data, int flagOffset)
    {
        if (flagOffset is < 0 or > 31)
        {
            throw new ArgumentOutOfRangeException("flagOffset");
        }

        return Has.Flag((uint)data, (byte)flagOffset);
        // var flag = data >> flagOffset;
        // return (flag & 1) == 1;
    }

    /// <summary>
    /// gets a range of bits
    /// </summary>
    /// <param name="data"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetBitRange(int data, int start, int end)
    {
        return (int)GetBitRange((uint)data, start, end);
    }

    /// <summary>
    /// gets a range of bits
    /// </summary>
    /// <param name="data"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetBitRange(uint data, int start, int end)
    {
        if (System.Runtime.Intrinsics.X86.Bmi1.IsSupported)
        {
            return System.Runtime.Intrinsics.X86.Bmi1.BitFieldExtract(data, (byte)start, (byte)end);
        }

        //shift binary to starting point of range
        var shifted = data >> start;

        //calculate range length (+1 for 0 index)
        var rangeLength = end - start + 1;

        //get binary mask based on range length

        uint maskBinary = rangeLength switch
        {
            1 =>  0b00000000_00000000_00000000_00000001,
            2 =>  0b00000000_00000000_00000000_00000011,
            3 =>  0b00000000_00000000_00000000_00000111,
            4 =>  0b00000000_00000000_00000000_00001111,
            5 =>  0b00000000_00000000_00000000_00011111,
            6 =>  0b00000000_00000000_00000000_00111111,
            7 =>  0b00000000_00000000_00000000_01111111,
            8 =>  0b00000000_00000000_00000000_11111111,
            9 =>  0b00000000_00000000_00000001_11111111,
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

        return shifted & maskBinary;
    }
}
