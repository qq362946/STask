using System;
using System.Diagnostics;

namespace Sining
{
    public partial class STask
    {
        public static STaskCompleted CompletedTask => new STaskCompleted();
        
        public static STask Run(Func<STask> factory)
        {
            return factory();
        }
        
        public static STask<T> Run<T>(Func<STask<T>> factory)
        {
            return factory();
        }
        
        public static STask<T> FromResult<T>(T value)
        {
            var sAwaiter = STask<T>.Create();
            sAwaiter.SetResult(value);
            return sAwaiter;
        }
    }
}