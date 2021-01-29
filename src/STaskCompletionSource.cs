using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace ConsoleApp1
{
    public sealed class STaskCompletionSource : IAwaiter
    {
        private bool _faulted;
        private Action _continuation;
        private ExceptionDispatchInfo _exception;
        [DebuggerHidden] public bool IsCompleted { get; private set; }
        [DebuggerHidden] public STask Task => new STask(this);
        [DebuggerHidden]
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation)
        {
            _continuation = continuation;
        }

        [DebuggerHidden]
        public void GetResult()
        {
            if (_faulted)
            {
                _exception?.Throw();
            }

            _faulted = false;
        }
        [DebuggerHidden]
        public void SetResult()
        {
            if (IsCompleted) return;
            IsCompleted = true;
            _continuation?.Invoke();
        }

        [DebuggerHidden]
        public void SetException(Exception e)
        {
            _exception = ExceptionDispatchInfo.Capture(e);
            _faulted = true;
            IsCompleted = true;
        }
    }
    
    public sealed class STaskCompletionSource<T> : IAwaiter<T>
    {
        private bool _faulted;
        private Action _continuation;
        private ExceptionDispatchInfo _exception;
        private T _result;
        [DebuggerHidden] public bool IsCompleted { get; private set; }
        [DebuggerHidden] public STask<T> Task => new STask<T>(this);
        [DebuggerHidden]
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation)
        {
            _continuation = continuation;
        }

        [DebuggerHidden]
        public T GetResult()
        {
            if (_faulted)
            {
                _exception?.Throw();
            }

            _faulted = false;
            return _result;
        }
        [DebuggerHidden]
        public void SetResult(T result)
        {
            if (IsCompleted) return;
            IsCompleted = true;
            _result = result;
            _continuation?.Invoke();
        }

        [DebuggerHidden]
        public void SetException(Exception e)
        {
            _exception = ExceptionDispatchInfo.Capture(e);
            _faulted = true;
            IsCompleted = true;
        }
    }
}