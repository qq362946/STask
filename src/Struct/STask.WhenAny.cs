using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    public partial struct STask
    {
        [DebuggerHidden]
        public static STask<STask> WhenAny(params STask[] tasks)
        {
            var sTaskCompletionSource = new STaskCompletionSource<STask>();

            foreach (var task in tasks)
            {
                RunSTask(sTaskCompletionSource, task).Coroutine();
            }

            return sTaskCompletionSource.Task;

            async SVoid RunSTask(STaskCompletionSource<STask> tcs, STask task)
            {
                await task;
                if (!tcs.IsCompleted)
                {
                    tcs.SetResult(task);
                }
            }
        }

        [DebuggerHidden]
        public static STask<STask<T>> WhenAny<T>(params STask<T>[] tasks)
        {
            var sTaskCompletionSource = new STaskCompletionSource<STask<T>>();

            foreach (var task in tasks)
            {
                RunSTask(sTaskCompletionSource, task).Coroutine();
            }

            return sTaskCompletionSource.Task;

            async SVoid RunSTask(STaskCompletionSource<STask<T>> tcs, STask<T> task)
            {
                await task;

                if (!tcs.IsCompleted) tcs.SetResult(task);
            }
        }
    }
}