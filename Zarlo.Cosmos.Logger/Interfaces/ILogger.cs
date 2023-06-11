namespace Zarlo.Cosmos.Logger.Interfaces;

public interface ILogger : IDisposable
{
    public void Info(string message);
    public void Info(string message, params object[] data);

    public void Error(string message);
    public void Error(string message, params object[] data);


    public void Exception(string message);
    public void Exception(string message, params object[] data);
    public void Exception(Exception exception, string message);
    public void Exception(Exception exception, string message, params object[] data);

    public void Debug(string message);
    public void Debug(string message, params object[] data);

    public void Trace(string message);
    public void Trace(string message, params object[] data);

    public void Raw(LogLevel logLevel, string message);
    public void Raw(LogLevel logLevel, string message, params object[] data);
}