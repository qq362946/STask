using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Sining
{
    public enum STaskStatus : byte
    {
        /// <summary>The operation has not yet completed.</summary>
        Pending = 0,
        /// <summary>The operation completed successfully.</summary>
        Succeeded = 1,
        /// <summary>The operation completed with an error.</summary>
        Faulted = 2,
    }
    
    [AsyncMethodBuilder(typeof(AsyncSTaskMethodBuilder))]
    public sealed partial class STask : INotifyCompletion
    {
        private STaskStatus _status;
        private bool _isFromPool;
        private Action _callBack;
        private ExceptionDispatchInfo _exception;

        public bool IsCompleted
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _status != STaskStatus.Pending;
        }
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public STask GetAwaiter() => this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerHidden]
        private async SVoid InnerCoroutine()
        {
            await this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerHidden]
        public void Coroutine()
        {
            InnerCoroutine().Coroutine();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetResult()
        {
            switch (_status)
            {
                case STaskStatus.Succeeded:
                    Recycle();
                    break;
                case STaskStatus.Faulted:
                    Recycle();
                    if (_exception != null)
                    {
                        var exception = _exception;
                        _exception = null;
                        exception.Throw();
                    }
                    break;
                default:
                    throw new NotSupportedException("Direct call to getResult is not allowed");
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Recycle()
        {
            if (!_isFromPool) return;

            _status = STaskStatus.Pending;
            _callBack = null;
            Pool<STask>.Return(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult()
        {
            if (_status != STaskStatus.Pending)
            {
                throw new InvalidOperationException("The task has been completed");
            }

            _status = STaskStatus.Succeeded;

            if (_callBack == null) return;

            var callBack = _callBack;
            _callBack = null;
            callBack.Invoke();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnsafeOnCompleted(Action continuation)
        {
            if (_status != STaskStatus.Pending)
            {
                continuation?.Invoke();
                return;
            }

            _callBack = continuation;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            if (_status != STaskStatus.Pending)
            {
                throw new InvalidOperationException("The task has been completed");
            }
            
            _status = STaskStatus.Faulted;
            _exception = ExceptionDispatchInfo.Capture(exception);
            _callBack?.Invoke();
        }
    }

    [AsyncMethodBuilder(typeof(AsyncSTaskMethodBuilder<>))]
    public sealed partial class STask<T> : INotifyCompletion
    {
        private STaskStatus _status;
        private bool _isFromPool;
        private Action _callBack;
        private ExceptionDispatchInfo _exception;
        private T _value;

        public bool IsCompleted
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _status != STaskStatus.Pending;
        }
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public STask<T> GetAwaiter() => this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerHidden]
        private async SVoid InnerCoroutine()
        {
            await this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerHidden]
        public void Coroutine()
        {
            InnerCoroutine().Coroutine();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetResult()
        {
            switch (_status)
            {
                case STaskStatus.Succeeded:
                    var value = _value;
                    Recycle();
                    return value;
                case STaskStatus.Faulted:
                    Recycle();
                    if (_exception == null) return default;
                    var exception = _exception;
                    _exception = null;
                    exception.Throw();
                    return default;
                default:
                    throw new NotSupportedException("Direct call to getResult is not allowed");
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Recycle()
        {
            if (!_isFromPool) return;

            _status = STaskStatus.Pending;
            _callBack = null;
            _value = default;
            Pool<STask<T>>.Return(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult(T value)
        {
            if (_status != STaskStatus.Pending)
            {
                throw new InvalidOperationException("The task has been completed");
            }

            _value = value;
            _status = STaskStatus.Succeeded;

            if (_callBack == null) return;

            var callBack = _callBack;
            _callBack = null;
            callBack.Invoke();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnsafeOnCompleted(Action continuation)
        {
            if (_status != STaskStatus.Pending)
            {
                continuation?.Invoke();
                return;
            }

            _callBack = continuation;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            if (_status != STaskStatus.Pending)
            {
                throw new InvalidOperationException("The task has been completed");
            }
            
            _status = STaskStatus.Faulted;
            _exception = ExceptionDispatchInfo.Capture(exception);
            _callBack?.Invoke();
        }
    }
}