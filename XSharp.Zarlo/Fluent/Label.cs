using XSharp.Assembler.x86;

namespace XSharp.Zarlo.Fluent;

public class Label
{
    private readonly string _label;

    public static Label Get(string label) => new Label(label);
    
    internal Label(string label)
    {
        _label = label;
    }

    public void Goto(ConditionalTestEnum conditionalTestEnum)
    {
        XS.Jump(conditionalTestEnum, _label);
    }
    
    public void Goto()
    {
        XS.Jump(_label);
    }
}

public static class LabelEx
{

    public static FluentXSharp Label(this FluentXSharp me, Action<Label> o) => me.Label(Guid.NewGuid().ToString(), o);
    
    public static FluentXSharp Label(this FluentXSharp me, string label, Action<Label> o)
    {
        me.Label(label, out var l);
        o(l);
        return me;
    }
    

    public static FluentXSharp Label(this FluentXSharp me, out Label o) => me.Label(Guid.NewGuid().ToString(), out o);
    
    public static FluentXSharp Label(this FluentXSharp me, string label, out Label o)
    {
        XS.Label(label);
        o = new Label(label);
        return me;
    }

    public static FluentXSharp Label(this FluentXSharp me, string label)
    {
        XS.Label(label);
        return me;
    }
    
    public static FluentXSharp Jump(this FluentXSharp me, Label label, ConditionalTestEnum conditionalTestEnum)
    {
        label.Goto(conditionalTestEnum);
        return me;
    }
    
    public static FluentXSharp Jump(this FluentXSharp me, Label label)
    {
        label.Goto();
        return me;
    }
    

    
}