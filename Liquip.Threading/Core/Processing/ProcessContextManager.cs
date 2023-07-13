using System;
using System.Collections.Generic;

using Cosmos.Core;

using Liquip.Threading.Core.Context;

namespace Liquip.Threading.Core.Processing;

public static unsafe class ProcessContextManager
{

    public const uint STACK_SIZE_THEAD = 102400;
    public const uint STACK_SIZE_PROCESS = 102400;

    public const uint STACK_SIZE_SYSCALL = 1024;
    public static uint m_NextCID;
    public static ProcessContext m_CurrentContext;
    public static ProcessContext m_ContextList;

    public static Mutex ContextListMutex = new Mutex();

    public static unsafe uint* GetContextPointer(uint tid)
    {
        return GCImplementation.GetPointer(GetContext(tid));
    }

    public static ProcessContext GetContext(uint tid)
    {
        ProcessContext ctx = m_ContextList;
        while (ctx.next != null)
        {
            if (ctx.tid == tid)
            {
                return ctx;
            }
            ctx = ctx.next;
        }
        if (ctx.tid == tid)
        {
            return ctx;
        }
        return null;
    }

    public static uint* SetupStack(uint* stack)
    {
        uint origin = (uint)stack;
        *--stack = 0xFFFFFFFF; // trash
        *--stack = 0xFFFFFFFF; // trash
        *--stack = 0xFFFFFFFF; // trash
        *--stack = 0xFFFFFFFF; // trash
        *--stack = 0x10; // ss ?
        *--stack = 0x00000202; // eflags
        *--stack = 0x8; // cs
        *--stack = ObjUtilities.GetEntryPoint(); // eip
        *--stack = 0; // error
        *--stack = 0; // int
        *--stack = 0; // eax
        *--stack = 0; // ebx
        *--stack = 0; // ecx
        *--stack = 0; // offset
        *--stack = 0; // edx
        *--stack = 0; // esi
        *--stack = 0; // edi
        *--stack = origin; //ebp
        *--stack = 0x10; // ds
        *--stack = 0x10; // fs
        *--stack = 0x10; // es
        *--stack = 0x10; // gs
        return stack;
    }

    public static LinkedList<ProcessContext> GetProcess()
    {

        var current = m_ContextList;

        var output = new LinkedList<ProcessContext>();

        while (current != null)
        {
            if (current.type != ProcessContextType.THREAD && current.state != ThreadState.DEAD)
            {
                output.AddLast(current);
            }
            current = current.next;
        }

        return output;
    }


    public static uint Count()
    {

        var current = m_ContextList;

        uint output = 0;

        while (current != null)
        {
            if (current.state != ThreadState.DEAD)
            {
                output++;
            }
            current = current.next;
        }

        return output;
    }

    public static uint StartContext(string name, ThreadStart entry, ProcessContextType type)
    {
        ProcessContext context = new ProcessContext();
        context.type = type;
        context.tid = m_NextCID++;
        context.name = name;
        switch (type)
        {
            case ProcessContextType.THREAD:
                context.stacktop = GCImplementation.AllocNewObject(STACK_SIZE_THEAD);
                break;
            case ProcessContextType.PROCESS:
            case ProcessContextType.PROCESS_FORK:
                context.stacktop = GCImplementation.AllocNewObject(STACK_SIZE_PROCESS);
                break;
            case ProcessContextType.SYSCALL:
                context.stacktop = GCImplementation.AllocNewObject(STACK_SIZE_SYSCALL);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        context.esp = (uint)SetupStack((uint*)(context.stacktop + 4000));
        context.state = ThreadState.PAUSED;
        context.entry = entry;
        if (type == ProcessContextType.PROCESS)
        {
            context.parent = 0;
        }
        else
        {
            context.parent = m_CurrentContext.tid;
        }
        // ContextListMutex.Lock();
        ProcessContext ctx = m_ContextList;
        while (ctx.next != null)
        {
            ctx = ctx.next;
        }
        ctx.next = context;
        GCImplementation.IncRootCount((ushort*)GCImplementation.GetPointer(context));
        // ContextListMutex.Unlock();
        return context.tid;
    }

    public static uint StartContext(string name, ParameterizedThreadStart entry, ProcessContextType type, object param)
    {
        ProcessContext context = new ProcessContext();
        context.type = type;
        context.tid = m_NextCID++;
        context.name = name;
        context.stacktop = GCImplementation.AllocNewObject(STACK_SIZE_THEAD);
        context.esp = (uint)SetupStack((uint*)(context.stacktop + 4000));
        context.state = ThreadState.ALIVE;
        context.paramentry = entry;
        context.param = param;
        if (type == ProcessContextType.PROCESS)
        {
            context.parent = 0;
        }
        else
        {
            context.parent = m_CurrentContext.tid;
        }
        // ContextListMutex.Lock();
        ProcessContext ctx = m_ContextList;
        while (ctx.next != null)
        {
            ctx = ctx.next;
        }
        ctx.next = context;
        GCImplementation.IncRootCount((ushort*)GCImplementation.GetPointer(context));
        // ContextListMutex.Unlock();
        return context.tid;
    }
}
