using System.Collections;
using System.Collections.Generic;
using Cosmos.Core;

namespace Liquip.Collections;

/// <summary>
/// a linked list
/// </summary>
/// <typeparam name="T"></typeparam>
public class ContextList<T>: IEnumerable<T>
{
    /// <summary>
    /// the current item in the list
    /// </summary>
    private CLinkedListItem<T>? current;

    // private Mutex Lock = new Mutex();

    private CLinkedListItem<T>? Head { get; set; }

    /// <summary>
    /// the total items in the list
    /// </summary>
    public uint Count { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public CLinkedListItem<T>? Current
    {
        get
        {
            current ??= Head;

            return current;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public T? CurrentItem => Current.Item;

    /// <summary>
    ///
    /// </summary>
    public void Next()
    {
        // Lock.Lock();
        if (current != null)
        {
            current = current.Next;
        }
        // Lock.Unlock();
    }

    /// <summary>
    /// go back 1 item
    /// </summary>
    public void Previous()
    {
        // Lock.Lock();
        current = current?.Previous;
        // Lock.Unlock();
    }

    /// <summary>
    /// adds an item to the list
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        // Lock.Lock();
        var newItem = new CLinkedListItem<T> { Item = item };

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

        Count++;
        // Lock.Unlock();
    }

    /// <summary>
    /// removes item from the list
    /// </summary>
    /// <param name="item"></param>
    public void Remove(T? item)
    {
        if (Head == null)
        {
            return;
        }

        // Lock.Lock();

        var node = Find(item);
        if (node != null)
        {
            node.Next.Previous = node.Previous;
            node.Previous.Next = node.Next;
        }

        GCImplementation.Free(node);
        Count--;
        // Lock.Unlock();
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

        if (index >= 0)
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

    /// <summary>
    /// Linked list item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CLinkedListItem<T>
    {
        /// <summary>
        /// value of the item
        /// </summary>
        public T? Item { get; set; }
        /// <summary>
        /// the next item of the list
        /// </summary>
        public CLinkedListItem<T>? Next { get; set; }
        /// <summary>
        /// the previous item of the list
        /// </summary>
        public CLinkedListItem<T>? Previous { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator()
    {
        var item = Head;
        while (item != null)
        {
            yield return item.Item;
            item = item.Next;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
