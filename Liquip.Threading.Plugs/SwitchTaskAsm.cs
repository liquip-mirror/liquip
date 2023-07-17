using IL2CPU.API;
using XSharp;
using XSharp.Assembler;
using Liquip.Core;
using Liquip.Threading.Core.Processing;
using static XSharp.XSRegisters;

namespace Liquip.Threading.Plugs;

public class SwitchTaskAsm
{
    public static void SwitchTask()
    {
        var StackContext = LabelName.GetStaticFieldName(typeof(ZINTs), nameof(ZINTs.mStackContext));
        var SwitchTaskMethod = XSharp.Utils.GetMethodDef(
            typeof(ProcessorScheduler),
            nameof(ProcessorScheduler.SwitchTask)
        );
        var SwitchTask = LabelName.Get(SwitchTaskMethod);
        _ = new LiteralAssemblerCode("pushad");
        _ = new LiteralAssemblerCode("mov eax, ds");
        _ = new LiteralAssemblerCode("push eax");
        _ = new LiteralAssemblerCode("mov eax, es");
        _ = new LiteralAssemblerCode("push eax");
        _ = new LiteralAssemblerCode("mov eax, fs");
        _ = new LiteralAssemblerCode("push eax");
        _ = new LiteralAssemblerCode("mov eax, gs");
        _ = new LiteralAssemblerCode("push eax");
        _ = new LiteralAssemblerCode("mov ax, 0x10");
        _ = new LiteralAssemblerCode("mov ds, ax");
        _ = new LiteralAssemblerCode("mov es, ax");
        _ = new LiteralAssemblerCode("mov fs, ax");
        _ = new LiteralAssemblerCode("mov gs, ax");
        _ = new LiteralAssemblerCode("mov eax, esp");
        XS.Set(StackContext, EAX, true);
        XS.Call(SwitchTask);
        XS.Set(EAX, StackContext, sourceIsIndirect: true);
        _ = new LiteralAssemblerCode("mov esp, eax");
        _ = new LiteralAssemblerCode("pop eax");
        _ = new LiteralAssemblerCode("mov gs, eax");
        _ = new LiteralAssemblerCode("pop eax");
        _ = new LiteralAssemblerCode("mov fs, eax");
        _ = new LiteralAssemblerCode("pop eax");
        _ = new LiteralAssemblerCode("mov es, eax");
        _ = new LiteralAssemblerCode("pop eax");
        _ = new LiteralAssemblerCode("mov ds, eax");
        _ = new LiteralAssemblerCode("popad");

    }
}
