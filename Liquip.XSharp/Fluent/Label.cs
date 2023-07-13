using IL2CPU.API;
using IL2CPU.API.Attribs;
using XSharp;
using XSharp.Assembler.x86;

namespace Liquip.XSharp.Fluent;

public class Label
{
    private readonly string _label;

    internal Label(string label)
    {
        _label = label;
    }

    public bool IsScoped => _label.StartsWith('.');

    public static Label Get(string label)
    {
        return new Label(label);
    }

    public static Label Get(AsmMarker.Type label)
    {
        return new Label(AsmMarker.Labels[label]);
    }


    public static Label GetFullName(Type type)
    {
        return new Label(LabelName.GetFullName(type));
    }

    public static Label GetFullName<T>()
    {
        return GetFullName(typeof(T));
    }


    public static Label New()
    {
        return new Label($@"FluentXSharp{Guid.NewGuid()}FluentXSharp");
    }


    public override string ToString()
    {
        return _label;
    }

    public override int GetHashCode()
    {
        return _label.GetHashCode();
    }

    /// <summary>
    /// </summary>
    /// <param name="conditionalTestEnum"></param>
    public void Goto(ConditionalTestEnum conditionalTestEnum)
    {
        XS.Jump(conditionalTestEnum, _label);
    }

    /// <summary>
    /// </summary>
    public void Goto()
    {
        XS.Jump(_label);
    }
}

public static class LabelEx
{
    public static FluentXSharpX86 Label(this FluentXSharpX86 me, Action<Label> o)
    {
        me.Label(out var label);
        o(label);
        return me;
    }

    public static FluentXSharpX86 Label(this FluentXSharpX86 me, string label, Action<Label> o)
    {
        me.Label(label, out var l);
        o(l);
        return me;
    }


    public static FluentXSharpX86 Label(this FluentXSharpX86 me, out Label o)
    {
        return me.Label($@"FluentXSharp{Guid.NewGuid()}FluentXSharp", out o);
    }

    public static FluentXSharpX86 Label(this FluentXSharpX86 me, string label, out Label o)
    {
        o = new Label(label);
        if (me.UsedLabels.Contains(o))
        {
            throw new Exception(string.Format("label in use {0}", o));
        }

        me.UsedLabels.Add(o);

        XS.Label(label);
        return me;
    }

    public static FluentXSharpX86 Label(this FluentXSharpX86 me, string label)
    {
        var o = new Label(label);
        if (me.UsedLabels.Contains(o))
        {
            throw new Exception(string.Format("label in use {0}", o));
        }

        me.UsedLabels.Add(o);
        XS.Label(label);
        return me;
    }

    public static FluentXSharpX86 Label(this FluentXSharpX86 me, Label label)
    {
        if (me.UsedLabels.Contains(label))
        {
            throw new Exception(string.Format("label in use {0}", label));
        }

        me.UsedLabels.Add(label);
        XS.Label(label.ToString());
        return me;
    }

    public static FluentXSharpX86 Jump(this FluentXSharpX86 me, Label label, ConditionalTestEnum conditionalTestEnum)
    {
        label.Goto(conditionalTestEnum);
        return me;
    }

    public static FluentXSharpX86 Jump(this FluentXSharpX86 me, Label label)
    {
        label.Goto();
        return me;
    }
}
