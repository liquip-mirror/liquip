using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Coroutines.System.Coroutines;
using NUnit.Framework;

namespace Liquip.Threading.Tasks.UNitTest;

public class Tests
{
    private bool isRunning = true;
    [Test]
    public void Run()
    {
        var thread1 = new Thread(CoroutinePool.Main.StartPool);
        thread1.Start();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));
        Main();
        while (isRunning)
        {
            cancellationTokenSource.Token.ThrowIfCancellationRequested();
        }
    }

    async KernelTask Main()
    {
        Assert.That(await Number() == 15);

        isRunning = false;
    }

    async KernelTask<int> Number()
    {
        return 15;
    }
}
