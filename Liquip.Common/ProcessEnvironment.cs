using System.Collections;
using System.Collections.Generic;

namespace Liquip.Common;

public class ProcessEnvironment: IEnumerable<KeyValuePair<string, string>>
{
    private Dictionary<string, string> data = new Dictionary<string, string>();
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => data.Count;

    public void Add(string key, string value) => data.Add(key, value);
    public void Add(string key, bool value) => data.Add(key, value.ToString());
    public void Add(string key, int value) => data.Add(key, value.ToString());

    public void Add(string key, IEnumerable<string> value) => data.Add(key, string.Join(";", value));
    public void Add(string key, params string[] value) => data.Add(key, string.Join(";", value));

    public string this[string key]
    {
        get => data[key];
        set
        {
            if (data.ContainsKey(key))
            {
                data[key] = value;
            }
            else
            {
                data.Add(key, value);
            }
        }
    }

}
