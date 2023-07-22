using Liquip.Threading.Core.Context;
using NUnit.Framework;

namespace Liquip.Threading.UNitTest;

public class Tests
{

    ProcessContext MakeContext(string name, ProcessContextType type, int stackSize, ProcessContext next) => new ProcessContext()
    {
        name = name,
        type = type,
        next = next,
    };

    void SetUp()
    {
        Liquip.Threading.Core.Processing.ProcessContextManager.m_ContextList = new ProcessContext()
        {
            name = "boot",
            type = ProcessContextType.PROCESS,

        };
    }

    [Test]
    public void InlineTest()
    {

    }
}
