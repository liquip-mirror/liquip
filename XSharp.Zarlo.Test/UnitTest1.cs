using IL2CPU.API;

namespace XSharp.Zarlo.Test;

public class Tests
{
    [Test]
    public void InlineTest()
    {
        _InlineTest(1, 4, "", "", 0x00, 15);

        void _InlineTest(
            int a,
            int b,
            string c,
            string d,
            byte e,
            short f
        )
        {
            var args = ArgumentBuilder.Inline();

            DoInlineTest(
                "string",
                args.Get(nameof(a)),
                args.Get(nameof(b)),
                args.Get(nameof(c)),
                args.Get(nameof(d)),
                args.Get(nameof(e)),
                args.Get(nameof(f))
            );

            DoInlineTest(
                "expression",
                args.Get(() => a),
                args.Get(() => b),
                args.Get(() => c),
                args.Get(() => d),
                args.Get(() => e),
                args.Get(() => f)
            );

            void DoInlineTest(
                string testName,
                int aArg,
                int bArg,
                int cArg,
                int dArg,
                int eArg,
                int fArg
            )
            {

                Console.WriteLine("{1} a: {0}", aArg, testName);
                Console.WriteLine("{1} b: {0}", bArg, testName);
                Console.WriteLine("{1} c: {0}", cArg, testName);
                Console.WriteLine("{1} d: {0}", dArg, testName);
                Console.WriteLine("{1} e: {0}", eArg, testName);
                Console.WriteLine("{1} f: {0}", fArg, testName);
                Assert.Multiple(() =>
                {

                    Assert.That(aArg, Is.EqualTo(4));
                    Assert.That(bArg, Is.EqualTo(8));
                    // if (Environment.Is64BitProcess)
                    // {
                    //     Assert.That(cArg, Is.EqualTo(16));
                    //     Assert.That(dArg, Is.EqualTo(24));
                    //     Assert.That(eArg, Is.EqualTo(25));
                    //     Assert.That(fArg, Is.EqualTo(27));
                    // }
                    // else
                    // {
                    Assert.That(cArg, Is.EqualTo(12));
                    Assert.That(dArg, Is.EqualTo(16));
                    Assert.That(eArg, Is.EqualTo(17));
                    Assert.That(fArg, Is.EqualTo(19));
                    // }
                });
            }
        }
    }

    [Test]
    public void Test2()
    {
        Console.WriteLine(GetNonRandomizedHashCode(""));
    }

    public static unsafe int GetNonRandomizedHashCode(string aString)
    {
        // the code is the same as the one used in .net except for the explicit == 2 and == 1 cases
        // we need this since a new object can start directly behind the string in memory, so the standard
        // implementation would read the allocated size of the next object and use it for the hash
        var asSpan = aString.AsSpan();
        if (asSpan.Length == 0)
        {
            unchecked
            {
                return (int)(352654597u + 352654597u * 1566083941);
            }
        }

        fixed (char* ptr = &asSpan[0])
        {
            uint num = 352654597u;
            uint num2 = num;
            uint* ptr2 = (uint*)ptr;
            int num3 = aString.Length;

            unchecked
            {
                while (num3 >= 4)
                {
                    num3 -= 4;
                    num = (global::System.Numerics.BitOperations.RotateLeft(num, 5) + num) ^ *ptr2;
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ ptr2[1];
                    ptr2 += 2;
                }
                if (num3 == 3)
                {
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ *ptr2;
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ ((char*)ptr2)[2];
                }
                else if (num3 == 2)
                {
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ *ptr2;
                }
                else if (num3 == 1)
                {
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ *(char*)ptr2;
                }
                return (int)(num + num2 * 1566083941);
            }
        }
    }
    
}