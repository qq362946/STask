using System;

namespace Sining
{
    public partial class STask
    {
        public static STask WhenAll(params STask[] tasks)
        {
            var count = tasks.Length;
            var sTaskCompletionSource = Create();

            foreach (var task in tasks)
            {
                RunSTask(sTaskCompletionSource, task).Coroutine();
            }

            return sTaskCompletionSource;

            async SVoid RunSTask(STask tcs, STask task)
            {
                await task;
                count--;
                if (count == 0)
                {
                    tcs.SetResult();
                }
            }
        }
    }

    public partial class STask<T>
    {
        public static STask WhenAll(params STask<T>[] tasks)
        {
            var count = tasks.Length;
            var sTaskCompletionSource = STask.Create();

            foreach (var task in tasks)
            {
                RunSTask(sTaskCompletionSource, task).Coroutine();
            }

            return sTaskCompletionSource;

            async SVoid RunSTask(STask tcs, STask<T> task)
            {
                await task;
                count--;
                if (count == 0)
                {
                    tcs.SetResult();
                }
            }
        }
    }
}