using System;

namespace Liquip.XSharp;

public class ImplementedInPlugException : NotImplementedException
{
    public ImplementedInPlugException()
    {
    }

    public ImplementedInPlugException(Type type) : base(type.FullName)
    {
    }
}
