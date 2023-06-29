using IL2CPU.API;
using XSharp;
using XSharp.Assembler;
using Zarlo.Cosmos.Core;
using Zarlo.Cosmos.Threading.Core.Processing;
using static XSharp.XSRegisters;

namespace Zarlo.Cosmos.Threading.Plugs;

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

        // var StackContext = LabelName.GetStaticFieldName(typeof(Zarlo.Cosmos.Core.ZINTs), nameof(Zarlo.Cosmos.Core.ZINTs.mStackContext));
        // var SwitchTaskMethod = Zarlo.XSharp.Utils.GetMethodDef(
        //     typeof(Core.Processing.ProcessorScheduler),
        //     nameof(Core.Processing.ProcessorScheduler.SwitchTask)
        // );
        // var SwitchTask = LabelName.Get(SwitchTaskMethod);
        // // PushAllRegisters
        // XS.PushAllRegisters();
        // var toPush = new XSRegisters.Register[] { DS, ES, FS, GS };
        // foreach (var register in toPush)
        // {
        //     XS.Set(EAX, register);
        //     XS.Push(EAX);
        // }
        // XS.Set(AX, 0x10);
        //
        // // set
        // var toMove = new XSRegisters.Register[] { DS, ES, FS, GS };
        // foreach (var register in toMove)
        // {
        //     XS.Set(register, AX);
        // }
        //
        // XS.Set(EAX, ESP);
        // XS.Set(StackContext, EAX, destinationIsIndirect: true);
        // XS.Call(SwitchTask);
        // XS.Set(EAX, StackContext, sourceIsIndirect: true);
        //
        // var toPop = new XSRegisters.Register[] { ESP, GS, FS, ES, DS };
        // foreach (var register in toPush)
        // {
        //     XS.Set(EAX, register);
        //     XS.Pop(EAX);
        // }
        // XS.PopAllRegisters();
    }
}
