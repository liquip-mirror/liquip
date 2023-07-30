using System;
using System.Runtime.CompilerServices;

namespace Liquip.Utils;

public static class Bits
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetBitRange(int data, int start, int end)
    {
        return (int)GetBitRange((uint)data, start, end);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetBitRange(uint data, int start, int end)
    {
        //shift binary to starting point of range
        var shifted = data >> start;

        //calculate range length (+1 for 0 index)
        var rangeLength = end - start + 1;

        //get binary mask based on range length

        uint maskBinary = rangeLength switch
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

        return shifted & maskBinary;
    }

}
