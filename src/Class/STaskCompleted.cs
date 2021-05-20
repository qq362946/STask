using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sining
{
    [AsyncMethodBuilder(typeof(AsyncSTaskCompletedMethodBuilder))]
    [StructLayout(LayoutKind.Auto)]
    public struct STaskCompleted : INotifyCompletion
    {
        [DebuggerHidden]
        public STaskCompleted GetAwaiter() => this;
        [DebuggerHidden] public bool IsCompleted => true;
        [DebuggerHidden]
        public void GetResult() { }
        [DebuggerHidden]
        public void OnCompleted(Action continuation) { }
        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation) { }
    }
}