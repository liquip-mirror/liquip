using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Liquip.WASM;

/// <summary>
///
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Value
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [FieldOffset(8)] public byte type;
    [FieldOffset(0)] public UInt32 i32;
    [FieldOffset(0)] public UInt64 i64;
    [FieldOffset(0)] public float f32;
    [FieldOffset(0)] public double f64;
    [FieldOffset(0)] public byte b0;
    [FieldOffset(1)] public byte b1;
    [FieldOffset(2)] public byte b2;
    [FieldOffset(3)] public byte b3;
    [FieldOffset(4)] public byte b4;
    [FieldOffset(5)] public byte b5;
    [FieldOffset(6)] public byte b6;
    [FieldOffset(7)] public byte b7;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static implicit operator Value(uint v) => From(v);

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static implicit operator Value(ulong v) => From(v);

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static implicit operator Value(float v) => From(v);

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static implicit operator Value(double v) => From(v);

    /// <summary>
    /// make a uint in to a value type
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Value From(uint v) => new Value
    {
        type = Type.i32,
        i32 = v
    };

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Value From(ulong v) => new Value
    {
        type = Type.i64,
        i64 = v
    };

    /// <summary>
    /// make a float in to a value type
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Value From(float v) => new Value
    {
        type = Type.f32,
        f32 = v
    };

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Value From(double v) => new Value
    {
        type = Type.f64,
        f64 = v
    };

    /// <summary>
    ///
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Value From(object v)
    {
        return v switch
        {
            uint u => From(u),
            ulong @ulong => From(@ulong),
            float f => From(f),
            double d => From(d),
            _ => throw new Exception()
        };
    }

}
