using System.Text;

namespace Liquip.WASM;
/// <summary>
/// wasm vm type
/// </summary>
public class WasmType
{
    // ReSharper disable InconsistentNaming
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public const byte i32 = 0x7F,
        i64 = 0x7E,
                      f32 = 0x7D,
                      f64 = 0x7C,
                      localGet = 0xFF,
                      localSet = 0xFE,
                      globalGet = 0xFD,
                      globalSet = 0xFC,
                      load32 = 0xFB,
                      store32 = 0xFA,
                      and32 = 0xF9,
                      add32 = 0xF8;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    // ReSharper restore InconsistentNaming

    public byte[] Parameters { get; init; }
    public byte[] Results { get; init; }

    public WasmType(byte[]? parameters, byte[]? results)
    {

        Parameters = parameters ?? Array.Empty<byte>();

        Results = results ?? Array.Empty<byte>();
    }

    public bool SameAs(WasmType item)
    {
        return Parameters.SequenceEqual(item.Parameters) && Results.SequenceEqual(item.Results);
    }

    public static string Pretify(WasmValue v)
    {
        return v.type switch
        {
            i32 => ((int)v.i32).ToString(),
            i64 => ((long)v.i64).ToString(),
            f32 => v.f32.ToString(),
            f64 => v.f64.ToString(),
            _ => "unknown (" + v.type + ")"
        };
    }

    public override string ToString()
    {

        var result = new StringBuilder();
        result.Append('(');

        for (var i = 0; i < Parameters.Length; i++)
        {
            switch (Parameters[i])
            {
                case 0x7F:
                    result.Append("i32");
                    break;
                case 0x7E:
                    result.Append("i64");
                    break;
                case 0x7D:
                    result.Append("f32");
                    break;
                case 0x7C:
                    result.Append("f64");
                    break;
                default:
                    result.Append("??");
                    break;
            }

            if (i + 1 < Parameters.Length)
            {
                result.Append(", ");
            }
        }

        result.Append(") => (");

        for (var i = 0; i < Results.Length; i++)
        {
            switch (Results[i])
            {
                case 0x7F:
                    result.Append("i32");
                    break;
                case 0x7E:
                    result.Append("i64");
                    break;
                case 0x7D:
                    result.Append("f32");
                    break;
                case 0x7C:
                    result.Append("f64");
                    break;
                default:
                    result.Append("??");
                    break;
            }

            if (i + 1 < Results.Length)
            {
                result.Append(", ");
            }
        }

        result.Append(')');

        return result.ToString();
    }
}
