using System;
using Liquip.Common.Exceptions;

namespace Liquip.WASM;

public class TrapException : LiquipException
{
    public string Details;

    public TrapException(string message, string details = "") : base(message)
    {
        Details = details;
    }
}
