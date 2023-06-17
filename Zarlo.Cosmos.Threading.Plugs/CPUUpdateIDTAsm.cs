using System;
using System.Reflection;

using Cosmos.Core;

using IL2CPU.API;
using IL2CPU.API.Attribs;

using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using Zarlo.XSharp;
using static XSharp.XSRegisters;


namespace Zarlo.Cosmos.Threading.Plugs;

public class CPUUpdateIDTAsm : AssemblerMethod
{

    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        // IDT is already initialized but just for base hooks, and asm only.
        // ie Int 1, 3 and GPF
        // This routine updates the IDT now that we have C# running to allow C# hooks to handle
        // the other INTs

        // We are updating the IDT, disable interrupts
        XS.ClearInterruptFlag();

        for (int i = 0; i < 256; i++)
        {
            // These are already mapped, don't remap them.
            // Maybe in the future we can look at ones that are present
            // and skip them, but some we may want to overwrite anyways.
            if (i == 1 || i == 3)
            {
                continue;
            }

            XS.Set(EAX, "__ISR_Handler_" + i.ToString("X2"));
            XS.Set("_NATIVE_IDT_Contents", AL, destinationDisplacement: i * 8 + 0);
            XS.Set("_NATIVE_IDT_Contents", AH, destinationDisplacement: i * 8 + 1);
            XS.Set("_NATIVE_IDT_Contents", 0x8, destinationDisplacement: i * 8 + 2, size: RegisterSize.Byte8);
            XS.Set("_NATIVE_IDT_Contents", 0x8E, destinationDisplacement: i * 8 + 5, size: RegisterSize.Byte8);
            XS.ShiftRight(EAX, 16);
            XS.Set("_NATIVE_IDT_Contents", AL, destinationDisplacement: i * 8 + 6);
            XS.Set("_NATIVE_IDT_Contents", AH, destinationDisplacement: i * 8 + 7);
        }

        XS.Jump("__AFTER__ALL__ISR__HANDLER__STUBS__");
        var xInterruptsWithParam = new[] { 8, 10, 11, 12, 13, 14 };
        for (int j = 0; j < 256; j++)
        {
            XS.Label("__ISR_Handler_" + j.ToString("X2"));
            // XS.Call("__INTERRUPT_OCCURRED__");

            if (Array.IndexOf(xInterruptsWithParam, j) == -1)
            {
                XS.Push(0);
            }
            XS.Push((uint)j);

            if (j != 0x20)
            {
                XS.PushAllRegisters();

                XS.Sub(ESP, 4);
                XS.Set(EAX, ESP); // preserve old stack address for passing to interrupt handler

                // store floating point data
                XS.And(ESP, 0xfffffff0); // fxsave needs to be 16-byte alligned
                XS.Sub(ESP, 512); // fxsave needs 512 bytes
                XS.SSE.FXSave(ESP, isIndirect: true); // save the registers
                XS.Set(EAX, ESP, destinationIsIndirect: true);

                XS.Push(EAX); //
                XS.Push(EAX); // pass old stack address (pointer to InterruptContext struct) to the interrupt handler

                XS.JumpToSegment(8, "__ISR_Handler_" + j.ToString("X2") + "_SetCS");
                XS.Label("__ISR_Handler_" + j.ToString("X2") + "_SetCS");
                MethodBase xHandler = Utils.GetInterruptHandler((byte)j);
                if (xHandler == null)
                {
                    xHandler = Utils.GetMethodDef(typeof(INTs), nameof(INTs.HandleInterrupt_Default), true);
                }
                XS.Call(LabelName.Get(xHandler));
                XS.Pop(EAX);
                XS.SSE.FXRestore(ESP, isIndirect: true);

                XS.Set(ESP, EAX); // this restores the stack for the FX stuff, except the pointer to the FX data
                XS.Add(ESP, 4); // "pop" the pointer

                XS.PopAllRegisters();
            }
            else
            {
                
                var StackContext = LabelName.GetStaticFieldName(typeof(Zarlo.Cosmos.Core.ZINTs), nameof(Zarlo.Cosmos.Core.ZINTs.mStackContext));
                var SwitchTaskMethod = Utils.GetMethodDef(
                    typeof(Zarlo.Cosmos.Threading.Core.Processing.ProcessorScheduler),
                    nameof(Zarlo.Cosmos.Threading.Core.Processing.ProcessorScheduler.SwitchTask)
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
                XS.Set(StackContext, EAX, destinationIsIndirect: true);
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
            
            XS.Add(ESP, 8);
            XS.Label("__ISR_Handler_" + j.ToString("X2") + "_END");
            XS.InterruptReturn();
        }
        // this looks useless
        // XS.Label("__INTERRUPT_OCCURRED__");
        // XS.Return();
        XS.Label("__AFTER__ALL__ISR__HANDLER__STUBS__");
        XS.Noop();
        XS.Set(EAX, EBP, sourceDisplacement: 8);
        XS.Compare(EAX, 0);
        XS.Jump(ConditionalTestEnum.Zero, ".__AFTER_ENABLE_INTERRUPTS");

        // reload interrupt list
        XS.Set(EAX, "_NATIVE_IDT_Pointer");
        XS.Set(AsmMarker.Labels[AsmMarker.Type.Processor_IntsEnabled], 1, destinationIsIndirect: true, size: RegisterSize.Byte8);
        XS.LoadIdt(EAX, isIndirect: true);
        // Reenable interrupts
        XS.EnableInterrupts();

        XS.Label(".__AFTER_ENABLE_INTERRUPTS");
    }
}