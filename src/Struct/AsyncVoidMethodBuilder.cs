﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    [StructLayout(LayoutKind.Auto)]
    public struct AsyncSVoidMethodBuilder
    {
        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncSVoidMethodBuilder Create() => default;
        // 2. TaskLike Task property(void)
        public SVoid Task => default;
        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
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
}