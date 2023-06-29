using Zarlo.Cosmos.Limine.Struct;

namespace Zarlo.Cosmos.Limine;

public class Limine
{
    private FramebufferObject _Framebuffer = new(0);
    private TerminalObject _Terminal = new(0);
    public ulong TerminalCount => TerminalObject.GetRaw.response.terminal_count;
    public ulong FramebufferCount => FramebufferObject.GetRaw.response.framebuffer_count;

    public TerminalObject GetTerminal()
    {
        return GetTerminal(0);
    }

    public TerminalObject GetTerminal(int index)
    {
        return new TerminalObject(index);
    }

    public TerminalObject GetTerminal(LimineTerminal terminal)
    {
        return new TerminalObject(terminal);
    }

    public FramebufferObject GetFramebuffer(int index = 0)
    {
        return new FramebufferObject(index);
    }

    public FramebufferObject GetFramebuffer(Framebuffer framebuffer)
    {
        return new FramebufferObject(framebuffer);
    }
}
