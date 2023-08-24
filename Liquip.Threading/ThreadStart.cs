namespace Liquip.Threading;

public delegate void ParameterizedThreadStart(object? obj);

public delegate void ThreadHandle(uint handle, object? obj);

public delegate void ThreadStart();
