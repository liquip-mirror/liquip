using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

using Cosmos.Core;
using Zarlo.Cosmos.Core;

namespace Zarlo.Cosmos.Threading.Core.Processing;

public static unsafe class ProcessContext
{
    public enum Thread_State
    {
        ALIVE = 0,
        DEAD = 1,
        WAITING_SLEEP = 2,
        PAUSED = 3,
        WAITING_IO = 4,
    }

    public enum Context_Type
    {
        THREAD = 0,
        PROCESS = 1,
        PROCESS_FORK = 2,
        SYSCALL = 3
    }

    public class Context
    {
        public Context next;
        public Context_Type type;
        public uint tid;
        public string name;
        public uint esp;
        public uint stacktop;
        public ThreadStart entry;
        public ParameterizedThreadStart paramentry;
        public Thread_State state;
        public object param;
        public int arg;
        public uint priority;
        public uint age;
        public uint parent;
    }

    public const uint STACK_SIZE = 4096;
    public static uint m_NextCID;
    public static Context m_CurrentContext;
    public static Context m_ContextList;

    public static Zarlo.Cosmos.Threading.Mutex ContextListMutex = new Mutex();

    public static Context GetContext(uint tid)
    {
        Context ctx = m_ContextList;
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

    public static LinkedList<Context> GetProcess()
    {

        var current = m_ContextList;

        var output = new LinkedList<Context>();

        while (current != null)
        {
            if (current.type != Context_Type.THREAD && current.state != Thread_State.DEAD)
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
            if (current.state != Thread_State.DEAD)
            {
                output++;
            }
            current = current.next;
        }

        return output;
    }

    public static uint StartContext(string name, ThreadStart entry, Context_Type type)
    {
        Context context = new Context();
        context.type = type;
        context.tid = m_NextCID++;
        context.name = name;
        context.stacktop = GCImplementation.AllocNewObject(STACK_SIZE);
        context.esp = (uint)SetupStack((uint*)(context.stacktop + 4000));
        context.state = Thread_State.PAUSED;
        context.entry = entry;
        if (type == Context_Type.PROCESS)
        {
            context.parent = 0;
        }
        else
        {
            context.parent = m_CurrentContext.tid;
        }
        ContextListMutex.Lock();
        Context ctx = m_ContextList;
        while (ctx.next != null)
        {
            ctx = ctx.next;
        }
        ctx.next = context;
        ContextListMutex.Unlock();
        return context.tid;
    }

    public static uint StartContext(string name, ParameterizedThreadStart entry, Context_Type type, object param)
    {
        Context context = new Context();
        context.type = type;
        context.tid = m_NextCID++;
        context.name = name;
        context.stacktop = GCImplementation.AllocNewObject(STACK_SIZE);
        context.esp = (uint)SetupStack((uint*)(context.stacktop + 4000));
        context.state = Thread_State.ALIVE;
        context.paramentry = entry;
        context.param = param;
        if (type == Context_Type.PROCESS)
        {
            context.parent = 0;
        }
        else
        {
            context.parent = m_CurrentContext.tid;
        }
        ContextListMutex.Lock();
        Context ctx = m_ContextList;
        while (ctx.next != null)
        {
            ctx = ctx.next;
        }
        ctx.next = context;
        ContextListMutex.Unlock();
        return context.tid;
    }
}