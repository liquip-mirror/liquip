using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cosmos.Core;
using Cosmos.Core.Memory;
using Zarlo.Cosmos.Threading;

namespace Zarlo.Cosmos.Collections;



public class ContextList<T>
{

    public class CLinkedListItem<T>
    {
        public T? Item { get; set; }
        public CLinkedListItem<T>? Next { get; set; }
        public CLinkedListItem<T>? Previous { get; set; }
    }

    private Mutex Lock = new Mutex();

    private CLinkedListItem<T>? Head { get; set; }

    private CLinkedListItem<T>? current = null;

    public uint Count => _Count;

    private uint _Count = 0;
    public unsafe CLinkedListItem<T>? Current { get {

            current ??= Head;

            return current;
        }
    }

    public void Next()
    {
        Lock.Lock();
        if (current != null)
        {
            current = current.Next;
        }
        Lock.Unlock();
    }

    public void Previous()
    {
        Lock.Lock();
        if (current != null)
        {
            current = current.Previous;
        }
        Lock.Unlock();
    }

    public void Add(T item)
    {
        Lock.Lock();
        var newItem = new CLinkedListItem<T>()
        {
            Item = item,
        };

        if (Head == null)
        {
            Head = newItem;
            Head.Next = null;
            Head.Previous = null;
        }
        else
        {
            if (Head.Previous == null)
            { 
                newItem.Previous = Head;
                Head.Previous = newItem;
            }
            else
            { 
                newItem.Previous = Head.Previous;
            }

            if (Head.Next == null)
            { 
                Head.Next = newItem;
            }
            else
            { 
                Head.Previous.Next = newItem;
            }

            Head.Previous = newItem;
        }
        _Count++;
        Lock.Unlock();
    }

    public void Remove(T? item)
    {
        if (Head == null) return;

        Lock.Lock();

        var node = Find(item);
        if (node != null)
        {
            node.Next.Previous = node.Previous;
            node.Previous.Next = node.Next;
        }
        GCImplementation.Free(node);
        _Count--;
        Lock.Unlock();
    }


    private CLinkedListItem<T>? Find(T? item)
    { 
        var current = Head;

        if (item == null)
        { 
            while (current != null)
            {
                if (current.Item == null)
                {
                    return current;
                }
                current = current.Next;
            }
        }
        else
        {
            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return current;
                }
                current = current.Next;
            }
        }
        return null;
    }

    private CLinkedListItem<T>? Find(int index)
    { 
        var current = Head;

        if (index == 0)
        {
            return current;
        }
        else if (index >= 0)
        {
            for (var i = 0; i < Count; i++)
            {
                if (current.Item == null)
                {
                    return current;
                }
                current = current.Next;
                if (Head.Equals(current))
                {
                    break;
                }
            }
        }
        else
        {
            var c = Count * -1;
            for (var i = 0; i < c; i--)
            {
                if (current.Item == null)
                {
                    return current;
                }
                current = current.Previous;
                if (Head.Equals(current))
                {
                    break;
                }
            }
        }
        return null;
    }

}
