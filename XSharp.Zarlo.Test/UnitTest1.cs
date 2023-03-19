using IL2CPU.API;

namespace XSharp.Zarlo.Test;

public class Tests
{
    [Test]
    public void Test1()
    {
        InlineTest(1,4);
    }
    
    public void InlineTest(int a, int b)
    {
        var args = ArgumentBuilder.Inline();
        var aArg = args.Get(nameof(a));
        var bArg = args.Get(nameof(b));
        Console.WriteLine(aArg);
        Console.WriteLine(bArg);
        Assert.Multiple(() =>
        {
            Assert.That(aArg, Is.EqualTo(4));
            Assert.That(bArg, Is.EqualTo(8));
        });
    }
}