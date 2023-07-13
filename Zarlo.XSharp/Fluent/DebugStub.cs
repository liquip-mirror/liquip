namespace Zarlo.XSharp.Fluent;

public static class DebugStub
{
    public static FluentXSharpX86 DebugStubSendCommandOnChannel(this FluentXSharpX86 me,
        string al,
        string bl,
        string ecx,
        string esi
        )
    {
        return me
            .IfDef("DEBUGSTUB", i => i
                .LiteralCode($"mov AL, {al}")
                .LiteralCode($"mov BL, {bl}")
                .LiteralCode($"mov ECX, {ecx}")
                .LiteralCode($"mov ESI, {esi}")
                .Call(Labels.DebugStub_SendCommandOnChannel)
            );
    }
}
