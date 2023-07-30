using System.Runtime.Serialization;

namespace Liquip.Common.Exceptions;


/// <summary>
/// Base Exception for Liquip
/// </summary>
public class LiquipException: Exception
{
    /// <inheritdoc />
    public LiquipException()
    {
    }

    /// <inheritdoc />
    protected LiquipException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <inheritdoc />
    public LiquipException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public LiquipException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
