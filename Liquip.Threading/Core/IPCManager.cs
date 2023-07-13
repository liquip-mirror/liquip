using System.Collections.Generic;
using Liquip.Collections;
using Liquip.Memory;
using Liquip.Threading.Core.Context;
using Liquip.Threading.Core.Processing;

namespace Liquip.Threading.Core;

public static class IPCManager
{
    public static Mutex Lock = new();

    public static ContextList<IpcContext> Context { get; set; }

    public static IpcContext? GetCurrent()
    {
        return GetContext(ProcessContextManager.m_CurrentContext.tid);
    }

    public static IpcContext GetContext(uint tid)
    {
        if (Context.Current == null)
        {
            return null;
        }

        Lock.Lock();
        for (var i = 0; i < Context.Count; i++)
        {
            if (Context.Current.Item.Tid == tid)
            {
                Lock.Unlock();
                return Context.Current.Item;
            }

            Context.Next();
        }

        Lock.Unlock();
        return null;
    }

    public static List<IpcContext> GetCurrentProcessContext()
    {
        var pid = ProcessContextManager.m_CurrentContext.parent;
        if (pid == 0)
        {
            pid = ProcessContextManager.m_CurrentContext.tid;
        }

        return GetProcessContext(pid);
    }

    public static List<IpcContext> GetProcessContext(uint pid)
    {
        if (Context.Current == null)
        {
            return new List<IpcContext>();
        }

        Lock.Lock();

        var output = new List<IpcContext>();
        for (var i = 0; i < Context.Count; i++)
        {
            if (Context.Current.Item.Pid == pid)
            {
                output.Add(Context.Current.Item);
            }

            Context.Next();
        }

        Lock.Unlock();
        return new List<IpcContext>();
    }

    public static void CreateContext()
    {
        var tid = ProcessContextManager.m_CurrentContext.tid;
        var pid = ProcessContextManager.m_CurrentContext.parent;
        CreateContext(pid, tid);
    }

    public static void CreateContext(uint pid, uint tid)
    {
        if (pid == 0)
        {
            pid = tid;
        }

        if (GetContext(tid) != null)
        {
            Lock.Lock();
            Context.Add(new IpcContext { Pid = pid, Tid = tid, Messages = new ContextList<IpcMessageContext>() });
            Lock.Unlock();
        }
    }

    public static void SendMessage(ref IpcContext context, ref Pointer message)
    {
        context.Messages.Add(
            new IpcMessageContext { From = ProcessContextManager.m_CurrentContext.tid, Data = message });
    }

    public static IpcMessageContext? GetCurrentMessage()
    {
        return GetMessage(ProcessContextManager.m_CurrentContext.tid);
    }

    public static IpcMessageContext? GetMessage(uint tid)
    {
        var context = GetContext(tid);
        var message = context.Messages.Current;

        if (message == null)
        {
            return null;
        }

        var messageItem = message.Item;

        context.Messages.Remove(messageItem);
        return messageItem;
    }

    public static bool CurrentHasMessage()
    {
        return HasMessage(ProcessContextManager.m_CurrentContext.tid);
    }

    public static bool HasMessage(uint tid)
    {
        var context = GetContext(tid);
        return context.Messages.Count > 0;
    }
}
