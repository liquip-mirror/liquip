using NUnit.Framework;

namespace Liquip.XSharp.UNitTest;

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
                    // short 2
                    Assert.That(fArg, Is.EqualTo(2));
                    // byte 1
                    Assert.That(eArg, Is.EqualTo(3));
                    // string 4
                    Assert.That(dArg, Is.EqualTo(7));
                    // string 4
                    Assert.That(cArg, Is.EqualTo(11));
                    // int 4
                    Assert.That(bArg, Is.EqualTo(15));
                    // int 4
                    Assert.That(aArg, Is.EqualTo(19));
                });
            }
        }
    }
}
