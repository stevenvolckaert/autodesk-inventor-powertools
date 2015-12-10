using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StevenVolckaert
{
    public static class TaskExtensions
    {
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "InCompletion", Justification = "Term is cased correctly.")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Rule does not apply to the task-based asynchronous programming pattern.")]
        public static IEnumerable<Task<T>> InCompletionOrder<T>(this IEnumerable<Task<T>> tasks)
        {
            var inputTasks = tasks.ToList();
            var sources = inputTasks.Select(x => new TaskCompletionSource<T>()).ToList();

            int nextTaskIndex = -1;

            foreach (var inputTask in inputTasks)
            {
                inputTask.ContinueWith(
                    completed =>
                    {
                        var source = sources[Interlocked.Increment(ref nextTaskIndex)];
                        if (completed.IsFaulted)
                            source.TrySetException(completed.Exception.InnerExceptions);
                        else if (completed.IsCanceled)
                            source.TrySetCanceled();
                        else
                            source.TrySetResult(completed.Result);
                    },
                    cancellationToken: CancellationToken.None,
                    continuationOptions: TaskContinuationOptions.ExecuteSynchronously,
                    scheduler: TaskScheduler.Default
                );
            }

            return sources.Select(x => x.Task);
        }
    }
}
