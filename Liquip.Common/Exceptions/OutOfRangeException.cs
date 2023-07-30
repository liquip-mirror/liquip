using System.Runtime.Serialization;

namespace Liquip.Common.Exceptions;


/// <summary>
/// ArgumentOutOfRangeException
/// </summary>
public class ArgumentOutOfRangeException: LiquipException
{
    /// <inheritdoc />
    public ArgumentOutOfRangeException()
    {
    }

    /// <inheritdoc />
    protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <inheritdoc />
    public ArgumentOutOfRangeException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public ArgumentOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
