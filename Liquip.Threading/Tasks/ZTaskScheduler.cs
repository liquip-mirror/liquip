// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// namespace Liquip.Threading.Tasks;
//
// public class ZTaskScheduler : TaskScheduler
// {
//     private Queue<Task> tasks = new Queue<Task>();
//
//     protected override IEnumerable<Task>? GetScheduledTasks()
//     {
//         return tasks.ToArray();
//     }
//
//     protected override void QueueTask(Task task)
//     {
//         tasks.Enqueue(task);
//     }
//
//     protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
//     {
//         tasks.Enqueue(task);
//     }
// }
