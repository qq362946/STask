using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    public interface IAwaiter : ICriticalNotifyCompletion
    {
        bool IsCompleted { get; }
        void GetResult();
    }
    
    public interface IAwaiter<T> : ICriticalNotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
        void SetResult(T result);
    }
    
    [StructLayout(LayoutKind.Auto)]
    public struct SAwaiter : IAwaiter
    {
        private ExceptionDispatchInfo _exception;
        public bool IsCompleted { get; private set; }
        [DebuggerHidden] public STask Task => new STask(this);

        [DebuggerHidden]
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation)
        {
            continuation?.Invoke();
        }

        [DebuggerHidden]
        public void GetResult() { }

        [DebuggerHidden]
        public void SetException(Exception e)
        {
            _exception = ExceptionDispatchInfo.Capture(e);
            IsCompleted = true;
            _exception.Throw();
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public struct SAwaiter<T> : IAwaiter<T>
    {
        public T _result;
        private ExceptionDispatchInfo _exception;
        public bool IsCompleted { get; private set; }
        [DebuggerHidden] public STask<T> Task => new STask<T>(this);

        [DebuggerHidden]
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation)
        {
            continuation?.Invoke();
        }

        [DebuggerHidden]
        public T GetResult()
        {
            return _result;
        }
        
        [DebuggerHidden]
        public void SetResult(T result)
        {
            _result = result;
        }

        [DebuggerHidden]
        public void SetException(Exception e)
        {
            _exception = ExceptionDispatchInfo.Capture(e);
            IsCompleted = true;
            _exception.Throw();
        }
    }
}