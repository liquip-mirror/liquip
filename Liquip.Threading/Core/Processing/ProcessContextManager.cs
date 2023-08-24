using System;
using System.Collections.Generic;

using Cosmos.Core;

using Liquip.Threading.Core.Context;
using Zarlo.Cosmos.Logger;
using Zarlo.Cosmos.Logger.Interfaces;

namespace Liquip.Threading.Core.Processing;

/// <summary>
///
/// </summary>
public static unsafe class ProcessContextManager
{

    /// <summary>
    ///
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public const uint STACK_SIZE_THEAD = 102400;

    /// <summary>
    ///
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public const uint STACK_SIZE_PROCESS = 102400;

    /// <summary>
    ///
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public const uint STACK_SIZE_SYSCALL = 1024;

    /// <summary>
    /// stores the last TID used
    /// </summary>
    public static uint NextCid = 0;

    /// <summary>
    /// stores the current context
    /// </summary>
    public static ProcessContext? CurrentContext = null;

    /// <summary>
    /// stores the first context
    /// </summary>
    public static ProcessContext ContextListHead;

    public static Mutex ContextListMutex = new Mutex();

    private static ILogger _logger = Log.GetLogger("Process Context Manager");

    public static ref ProcessContext GetContext(uint tid)
    {
        _logger.Info("looking for context");
        for (var node = ContextListHead; node != null; node = node.Next)
        {
            if (node.Id == tid)
            {
                return ref node;
            }
        }

        return ref ProcessContext.NULL;
    }

    /// <summary>
    /// sets up the default stack
    /// </summary>
    /// <param name="stack">pointer to the stack</param>
    /// <returns></returns>
    public static uint* SetupStack(uint* stack)
    {
        var origin = (uint)stack;
        *--stack = 0xFF_FF_FF_FF; // trash
        *--stack = 0xFF_FF_FF_FF; // trash
        *--stack = 0xFF_FF_FF_FF; // trash
        *--stack = 0xFF_FF_FF_FF; // trash
        *--stack = 0x10; // ss ?
        *--stack = 0x00_00_02_02; // eflags
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

    /// <summary>
    /// Counts all none dead tasks
    /// </summary>
    /// <returns></returns>
    public static uint Count()
    {

        var current = ContextListHead;

        uint output = 0;

        while (current != null)
        {
            if (current.State != ThreadState.DEAD)
            {
                output++;
            }
            current = current.Next;
        }

        return output;
    }

    /// <summary>
    /// starts a new thread/process with no args
    /// </summary>
    /// <param name="name"></param>
    /// <param name="entry"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static uint StartContext(string name, ThreadStart entry, ProcessContextType type)
    {
        _logger.Info("starting new context");
        ProcessContext context = new ProcessContext();
        context.Type = type;
        context.Id = NextCid++;
        context.Name = name;
        switch (type)
        {
            case ProcessContextType.THREAD:
                context.Stacktop = GCImplementation.AllocNewObject(STACK_SIZE_THEAD);
                break;
            case ProcessContextType.PROCESS:
            case ProcessContextType.PROCESS_FORK:
                context.Stacktop = GCImplementation.AllocNewObject(STACK_SIZE_PROCESS);
                break;
            case ProcessContextType.SYSCALL:
                context.Stacktop = GCImplementation.AllocNewObject(STACK_SIZE_SYSCALL);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        context.ESP = (uint)SetupStack((uint*)(context.Stacktop + 4000));
        context.State = ThreadState.PAUSED;
        context.Entry = entry;
        if (type == ProcessContextType.PROCESS)
        {
            context.ParentId = 0;
        }
        else
        {
            context.ParentId = CurrentContext.Id;
        }
        ContextListMutex.Lock();
        for (var node = ContextListHead; node != null; node = node.Next)
        {
            if (node.Next == null)
            {
                node.Next = context;
            }
        }
        GCImplementation.IncRootCount((ushort*)GCImplementation.GetPointer(context));
        ContextListMutex.Unlock();

        if (ContextListHead == null)
        {
            ContextListHead = context;
        }

        if (CurrentContext == null)
        {
            CurrentContext = ContextListHead;
        }

        return context.Id;
    }


    /// <summary>
    /// starts a thread/process with args
    /// </summary>
    /// <param name="name"></param>
    /// <param name="entry"></param>
    /// <param name="type"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static uint StartContext(string name, ParameterizedThreadStart entry, ProcessContextType type, object param)
    {
        _logger.Info("starting new context");
        ProcessContext context = new ProcessContext();
        context.Type = type;
        context.Id = NextCid++;
        context.Name = name;
        switch (type)
        {
            case ProcessContextType.THREAD:
                context.Stacktop = GCImplementation.AllocNewObject(STACK_SIZE_THEAD);
                break;
            case ProcessContextType.PROCESS:
            case ProcessContextType.PROCESS_FORK:
                context.Stacktop = GCImplementation.AllocNewObject(STACK_SIZE_PROCESS);
                break;
            case ProcessContextType.SYSCALL:
                context.Stacktop = GCImplementation.AllocNewObject(STACK_SIZE_SYSCALL);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        context.ESP = (uint)SetupStack((uint*)(context.Stacktop + 4000));
        context.State = ThreadState.PAUSED;
        context.ParamEntry = entry;
        context.Param = param;
        if (type == ProcessContextType.PROCESS)
        {
            context.ParentId = 0;
        }
        else
        {
            context.ParentId = CurrentContext.Id;
        }
        ContextListMutex.Lock();
        for (var node = ContextListHead; node != null; node = node.Next)
        {
            if (node.Next == null)
            {
                node.Next = context;
            }
        }
        GCImplementation.IncRootCount((ushort*)GCImplementation.GetPointer(context));
        ContextListMutex.Unlock();

        if (ContextListHead == null)
        {
            ContextListHead = context;
        }

        if (CurrentContext == null)
        {
            CurrentContext = ContextListHead;
        }
        return context.Id;
    }
}
