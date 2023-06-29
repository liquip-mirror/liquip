namespace Zarlo.XSharp;

public class ImplementedInPlugException : NotImplementedException
{
    public ImplementedInPlugException()
    {
    }

    public ImplementedInPlugException(Type type) : base(type.FullName)
    {
    }
}
