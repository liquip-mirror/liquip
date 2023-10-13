using Liquip.Memory;

namespace Liquip.WASM;

/// <summary>
///
/// </summary>
public class Leb128
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public long GetSigned(ref Pointer ptr, int offset, out byte size)
    {
        ulong result = 0;
        byte shift = 0;
        byte b;
        var index = (uint)offset;
        size = 0;
        do
        {
            size++;
            unsafe
            {
                b = (byte)(byte*)ptr.Ptr[index++];
            }

            result |= ((ulong)0x7F & b) << shift;
            shift += 7;

        } while ((b & 0x80) != 0);

        /* sign bit of byte is second high order bit (0x40) */
        if ((b & 0x40) != 0)
        {
            result |= ~(ulong)0 << shift;
        }

        return (long)result;
    }

}
