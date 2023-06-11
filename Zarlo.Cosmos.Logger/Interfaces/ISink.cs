namespace Zarlo.Cosmos.Logger.Interfaces;

public interface ISink : IDisposable
{
    public void Raw(string context, LogLevel logLevel, string message);
    public void Raw(string context, LogLevel logLevel, string message, params object[] data);
}