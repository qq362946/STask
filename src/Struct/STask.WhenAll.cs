using System;

namespace ConsoleApp1
{
    public partial struct STask
    {
        public static STask WhenAll(params STask[] tasks)
        {
            var count = tasks.Length;
            var sTaskCompletionSource = new STaskCompletionSource();

            foreach (var task in tasks)
            {
                RunSTask(sTaskCompletionSource, task).Coroutine();
            }

            return sTaskCompletionSource.Task;

            async SVoid RunSTask(STaskCompletionSource tcs, STask task)
            {
                await task;
                count--;
                if (count == 0)
                {
                    tcs.SetResult();
                }
            }
        }

        public static STask WhenAll<T>(params STask<T>[] tasks)
        {
            var count = tasks.Length;
            var sTaskCompletionSource = new STaskCompletionSource();

            foreach (var task in tasks)
            {
                RunSTask(sTaskCompletionSource, task).Coroutine();
            }

            return sTaskCompletionSource.Task;

            async SVoid RunSTask(STaskCompletionSource tcs, STask<T> task)
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