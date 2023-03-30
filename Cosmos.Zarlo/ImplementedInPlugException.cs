namespace Cosmos.Zarlo;

public class ImplementedInPlugException : NotImplementedException
{
    public ImplementedInPlugException() : base()
    {
    }

    public ImplementedInPlugException(Type type) : base(type.FullName)
    {
    }
}