using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    [AsyncMethodBuilder(typeof(AsyncSTaskMethodBuilder))]
    [StructLayout(LayoutKind.Auto)]
    public readonly partial struct STask
    {
        private readonly IAwaiter Awaiter;

        [DebuggerHidden]
        public IAwaiter GetAwaiter() => Awaiter;

        [DebuggerHidden]
        public STask(IAwaiter awaiter)
        {
            Awaiter = awaiter;
        }

        [DebuggerHidden]
        public STask(IAwaiter awaiter, Action action)
        {
            Awaiter = awaiter;
        }
    }

    [AsyncMethodBuilder(typeof(AsyncSTaskMethodBuilder<>))]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct STask<T>
    {
        private readonly IAwaiter<T> Awaiter;

        [DebuggerHidden]
        public IAwaiter<T> GetAwaiter() => Awaiter;

        [DebuggerHidden]
        public STask(IAwaiter<T> awaiter)
        {
            Awaiter = awaiter;
        }
    }
}