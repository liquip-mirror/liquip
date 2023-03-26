using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IL2CPU.API;

namespace XSharp.Zarlo;

public unsafe class ArgumentBuilder
{

    public static ArgumentBuilder Inline() => Inline(2);
    
    public static ArgumentBuilder Inline(int i)
    {
        var stackTrace = new StackTrace();
        var methodInfo = stackTrace.GetFrame(i).GetMethod()!;

        // if (!methodInfo.CustomAttributes
        //         .Any(i => i.AttributeType == typeof(InlineAttribute))
        //     )
        // {
        //     throw new Exception("can only be called form methods with InlineAttribute");
        // }

        var builder = new ArgumentBuilder();

        var parameters= methodInfo.GetParameters();

        foreach (var parameter in parameters)
        {
            builder.Add(parameter.ParameterType, parameter.Name);
        }
        
        return builder;
    }

    private readonly List<(string Name, int Size)> _index = new List<(string Name, int Size)>();
    
    public unsafe void Add<T>(string name, uint? index = null)
    {
        Add(typeof(T), name, index);
    }
    
    public unsafe void Add(Type type, string name, uint? index = null)
    {
        uint size = 0;
        if (type.IsClass)
        {
            size = 4; // hard coded for now
            //size = (uint)IntPtr.Size;
        }
        else
        {
            size = (uint)Marshal.SizeOf(type);
        }

        Add(size, name, index);
    }
    
    public void Add(uint typeSize, string name, uint? index = null)
    {
        if (index.HasValue)
        {
            _index.Insert((int)index, (name, (int)typeSize));
        }
        else
        {
            _index.Add((name, (int)typeSize));
        }
    }

    [Pure]
    public int Get<T>(Expression<Func<T>> expr)
    {
        return Get((expr.Body as MemberExpression)?.Member.Name);
    }

    public int Get(string name)
    {
        var i = 0;
        var found = false;
        foreach (var item in _index)
        {
            i += item.Size;
            if (item.Name == name)
            {
                found = true;
                break;
            }
        }

        if (!found) throw new ArgumentException($@"argument not found {name}");

        return i;
        
    }
    
    [Pure]
    public string GetOffset(Expression<Func<object, object>> f)
    {
        return GetOffset((f.Body as MemberExpression)?.Member.Name);
    }
    public string GetOffset(string name)
    {
        return $@"[esp + {Get(name)}]";
    }

}
