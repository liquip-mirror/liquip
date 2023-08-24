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
//     public ZTask(Action action)
//     {
//     }
//
//     public ZTask(Action action, CancellationToken cancellationToken)
//     {
//     }
//
//     public ZTask(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
//     {
//     }
//
//     public ZTask(Action action, TaskCreationOptions creationOptions)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state, CancellationToken cancellationToken)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
//     {
//     }
//
//     public ZTask(Action<object?> action, object? state, TaskCreationOptions creationOptions)
//     {
//     }
// }
