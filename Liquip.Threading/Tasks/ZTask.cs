// using System;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace Liquip.Threading.Tasks;
//
// public class ZTask
// {
//
//     public static readonly ThreadStatic<ZTask> CurrentTask = new ThreadStatic<ZTask>();
//
//     public static int? CurrentId => ZTask.CurrentTask.Value?.Id;
//
//     private static int NextId = 0;
//
//     public int Id { get; } = NextId++;
//
//     public ZTask(Action action) : base(action)
//     {
//     }
//
//     public ZTask(Action action, CancellationToken cancellationToken) : base(action, cancellationToken)
//     {
//     }
//
//     public ZTask(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(action, cancellationToken, creationOptions)
//     {
//     }
//
//     public ZTask(Action action, TaskCreationOptions creationOptions) : base(action, creationOptions)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state) : base(action, state)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state, CancellationToken cancellationToken) : base(action, state, cancellationToken)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(action, state, cancellationToken, creationOptions)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state, TaskCreationOptions creationOptions) : base(action, state, creationOptions)
//     {
//     }
// }
