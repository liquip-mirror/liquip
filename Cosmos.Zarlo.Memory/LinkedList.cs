using System.Collections;
using System.Runtime.CompilerServices;
using Cosmos.Core;

namespace Cosmos.Zarlo.Memory;


public class LinkedListNode<T>
{

    public T Data { get; set; }
    public LinkedListNode<T>? Next { get; set; }
    public LinkedListNode<T>? Previous { get; set; }

}


public class LinkedList<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.ICollection
{
    private int _count = 0;

    LinkedListNode<T>? _start { get; set; }
    LinkedListNode<T>? _end { get; set; }


    public int Count => _count;
    public bool IsReadOnly => false;

    public bool IsSynchronized => throw new NotImplementedException();

    public object SyncRoot => throw new NotImplementedException();

    public void Add(T item)
    {
        var newitem = new LinkedListNode<T>()
        {
            Data = item,
            Previous = _end
        };
        _end.Next = newitem;
        _end = newitem;
        _count++;
    }

    public bool Remove(T item)
    {
        var next = _start;
        while (next != null)
        {
            var current = next;
            next = current.Next;
            var remove = false;
            if (current.Data == null)
            {
                if (item == null)
                {
                    remove = true;
                }
            }
            if (current.Data.Equals(item))
            {
                remove = true;
            }
            if (remove)
            { 
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
                GCImplementation.Free(current);
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        var next = _start;
        while (next != null)
        {
            var current = next;
            next = current.Next;
            GCImplementation.Free(current);
        }
        _start = null;
        _end = null;
        _count = 0;
    }

    public bool Contains(T item)
    {
        var next = _start;
        while (next != null)
        {
            var current = next;
            next = current.Next;
            var remove = false;
            if (current.Data == null)
            {
                if (item == null)
                {
                    remove = true;
                }
            }
            if (current.Data.Equals(item))
            {
                remove = true;
            }
            if (remove)
            { 
                return true;
            }
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        var next = _start;
        int i = 0;
        while (next != null)
        {
            var current = next;
            next = current.Next;
            array[arrayIndex + i] = current.Data;
            i++;
        }
    }

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }


}