using Cosmos.Zarlo.Limine.Struct;

namespace Cosmos.Zarlo.Limine;
public class Limine
{

    private TerminalObject _Terminal = new TerminalObject(0);
    public ulong TerminalCount => TerminalObject.GetRaw.response.terminal_count;
    public TerminalObject GetTerminal() => GetTerminal(0);
    public TerminalObject GetTerminal(int index) => new TerminalObject(index);
    public TerminalObject GetTerminal(LimineTerminal terminal) => new TerminalObject(terminal);

    private FramebufferObject _Framebuffer = new FramebufferObject(0);
    public ulong FramebufferCount => FramebufferObject.GetRaw.response.framebuffer_count;
    public FramebufferObject GetFramebuffer(int index = 0) => new FramebufferObject(index);
    public FramebufferObject GetFramebuffer(Framebuffer framebuffer) => new FramebufferObject(framebuffer);


}
