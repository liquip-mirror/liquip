﻿using System.Collections;

namespace Zarlo.Cosmos.FileSystems.NTFS.Utility;

public class CompatDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private readonly List<KeyValuePair<TKey, TValue>> list = new();

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        list.Add(item);
    }

    public void Clear()
    {
        list.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return list.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return list.Remove(item);
    }

    public int Count => list.Count;
    public bool IsReadOnly => false;

    public void Add(TKey key, TValue value)
    {
        list.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
        KeyValuePair<TKey, TValue>? itm2remove = null;
        foreach (var kvp in list)
        {
            if (kvp.Key.Equals(key))
            {
                itm2remove = kvp;
                break;
            }
        }

        if (itm2remove.HasValue)
        {
            return list.Remove(itm2remove.Value);
        }

        return false;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        foreach (var kvp in list)
        {
            if (kvp.Key.Equals(key))
            {
                value = kvp.Value;
                return true;
            }
        }

        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            foreach (var kvp in list)
            {
                if (kvp.Key.Equals(key))
                {
                    return kvp.Value;
                }
            }

            throw new Exception("no such key");
        }
        set
        {
            for (var i = 0; i < list.Count; i++)
            {
                var kvp = list[i];
                if (kvp.Key.Equals(key))
                {
                    list[i] = new KeyValuePair<TKey, TValue>(kvp.Key, value);
                }
            }
        }
    }

    public ICollection<TKey> Keys { get; }

    public ICollection<TValue> Values
    {
        get
        {
            var values = new List<TValue>();
            foreach (var v in list)
            {
                values.Add(v.Value);
            }

            return values;
        }
    }
}
