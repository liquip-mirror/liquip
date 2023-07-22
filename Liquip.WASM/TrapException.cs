using System;

namespace Liquip.WASM;

public class TrapException : Exception
{
    public string Details;

    public TrapException(string message, string details = "") : base(message)
    {
        Details = details;
    }
}
