using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    [StructLayout(LayoutKind.Auto)]
    public struct AsyncSTaskMethodBuilder
    {
        private readonly SAwaiter _sAwaiter;
        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncSTaskMethodBuilder Create() => new AsyncSTaskMethodBuilder(new SAwaiter());
        [DebuggerHidden]
        private AsyncSTaskMethodBuilder(SAwaiter sAwaiter)
        {
            _sAwaiter = sAwaiter;
        }
        // 2. TaskLike Task property(void)
        public STask Task => _sAwaiter.Task;
        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
            _sAwaiter.SetException(exception);
        }
        // 4. SetResult
        [DebuggerHidden]
        public void SetResult() { }
        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }
        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.UnsafeOnCompleted(stateMachine.MoveNext);
        }
        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }
        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine){}
    }
    
    [StructLayout(LayoutKind.Auto)]
    public struct AsyncSTaskMethodBuilder<T>
    {
        private SAwaiter<T> _sAwaiter;
        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncSTaskMethodBuilder<T> Create() => new AsyncSTaskMethodBuilder<T>(new SAwaiter<T>());
        [DebuggerHidden]
        private AsyncSTaskMethodBuilder(SAwaiter<T> sAwaiter)
        {
            _sAwaiter = sAwaiter;
        }
        // 2. TaskLike Task property(void)
        public STask<T> Task => _sAwaiter.Task;
        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
            _sAwaiter.SetException(exception);
        }
        // 4. SetResult
        [DebuggerHidden]
        public void SetResult(T result) 
        {
            // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
            _sAwaiter.SetResult(result);
        }
        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }
        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.UnsafeOnCompleted(stateMachine.MoveNext);
        }
        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }
        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine){}
    }
}