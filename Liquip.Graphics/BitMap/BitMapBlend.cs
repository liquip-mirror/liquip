using Liquip.Memory;

namespace Liquip.Graphics.BitMap;

public class BitMapBlend
{
    public static Pointer Blend(Pointer a, Pointer b, byte factor)
    {

        var aAddress = a.ToInt();
        var bAddress = b.ToInt();
        var size = a.Size;
        var output = Pointer.New(size);
        var outAddress = output.ToInt();

        switch (factor)
        {
            case 0:
                a.CopyTo(output, 0, 0, output.Size);
                break;
            case 0xff:
                b.CopyTo(output, 0, 0, output.Size);
                break;
            default:
            {
                for (int i = 0; i < a.Size; i++)
                {

                }

                break;
            }
        }

        return output;
    }

}
