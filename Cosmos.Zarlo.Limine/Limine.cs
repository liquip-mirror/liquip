using Cosmos.Zarlo.Limine.Struct;

namespace Cosmos.Zarlo.Limine;
public class Limine
{

    private TerminalObject _Terminal = new TerminalObject(0);
    public TerminalObject GetTerminal() => GetTerminal(0);
    public TerminalObject GetTerminal(int index) => new TerminalObject(index);
    public TerminalObject GetTerminal(LimineTerminal terminal) => new TerminalObject(terminal);

    private FramebufferObject _Framebuffer = new FramebufferObject(0);
    public FramebufferObject GetFramebufferO() => GetFramebufferO(0);
    public FramebufferObject GetFramebufferO(int index) => new FramebufferObject(index);
    public FramebufferObject GetFramebufferO(Framebuffer framebuffer) => new FramebufferObject(framebuffer);


}
