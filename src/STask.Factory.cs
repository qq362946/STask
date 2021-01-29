using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    public partial struct STask
    {
        [DebuggerHidden] public static STaskCompleted CompletedTask => new STaskCompleted();

        [DebuggerHidden]
        public static STask Run(Func<STask> factory)
        {
            return factory();
        }

        [DebuggerHidden]
        public static STask<T> Run<T>(Func<STask<T>> factory)
        {
            return factory();
        }

        public static STask<T> FromResult<T>(T value)
        {
            var sAwaiter = new SAwaiter<T>();
            sAwaiter.SetResult(value);
            return sAwaiter.Task;
        }
    }
}