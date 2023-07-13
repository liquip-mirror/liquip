using Liquip.Limine.Struct;

namespace Liquip.Limine;

public class TerminalObject
{
    private static readonly TerminalRequest terminalRequest = new()
    {
        id = new ulong[] { 0xc7b1dd30df4c8b88, 0x0a82e883a194f07b, 0xc8ac59310c2b0844, 0xa68d0c7265d38878 },
        revision = 0
    };

    private readonly int _id;
    private readonly LimineTerminal _terminal;

    public TerminalObject(int id)
    {
        _id = id;
        _terminal = terminalRequest.response.terminals[_id];
    }

    public TerminalObject(LimineTerminal terminal)
    {
        _terminal = terminal;
    }

    public static TerminalRequest GetRaw => terminalRequest;

    public ulong Columns => _terminal.columns;
    public ulong Rows => _terminal.rows;

    public void Write(string data)
    {
        terminalRequest.response.write(_terminal, data, (ulong)data.Length);
    }

    public FramebufferObject GetFramebuffer()
    {
        return new FramebufferObject(_terminal.framebuffer);
    }
}
