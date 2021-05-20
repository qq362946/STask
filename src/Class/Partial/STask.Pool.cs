using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sining
{
    public partial class STask
    {
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static STask Create(bool isFromPool = true)
        {
            var task = isFromPool ? Pool<STask>.Rent() : new STask();
            task._isFromPool = isFromPool;
            return task;
        }
    }

    public partial class STask<T>
    {
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static STask<T> Create(bool isFromPool = true)
        {
            var task = isFromPool ? Pool<STask<T>>.Rent() : new STask<T>();
            task._isFromPool = isFromPool;
            return task;
        }
    }
}