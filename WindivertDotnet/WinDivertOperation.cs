﻿using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace WindivertDotnet
{
    /// <summary>
    /// Windivert控制器
    /// </summary>
    [SupportedOSPlatform("windows")]
    abstract unsafe class WinDivertOperation : IDisposable, IValueTaskSource<int>
    {
        private readonly ThreadPoolBoundHandle boundHandle;
        private readonly NativeOverlapped* nativeOverlapped;

        private ManualResetValueTaskSourceCore<int> taskSource; // 不能readonly
        private static readonly IOCompletionCallback completionCallback = new(IOCompletionCallback);

        /// <summary>
        /// Windivert控制器
        /// </summary>
        /// <param name="divert"></param> 
        public WinDivertOperation(WinDivert divert)
        {
            this.boundHandle = divert.GetThreadPoolBoundHandle();
            this.nativeOverlapped = this.boundHandle.AllocateNativeOverlapped(completionCallback, this, null);
        }

        /// <summary>
        /// io控制
        /// </summary> 
        public virtual ValueTask<int> IOControlAsync()
        {
            var length = 0; // 如果触发异步回调，回调里不会反写pLength，所以这里可以声明为方法内部变量
            return this.IOControl(&length, this.nativeOverlapped)
                ? new ValueTask<int>(length)
                : new ValueTask<int>(this, this.taskSource.Version);
        }

        /// <summary>
        /// io控制
        /// </summary>
        /// <param name="pLength"></param> 
        /// <param name="nativeOverlapped"></param>
        /// <returns></returns>
        protected abstract bool IOControl(int* pLength, NativeOverlapped* nativeOverlapped);

        /// <summary>
        /// io完成回调
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="numBytes"></param>
        /// <param name="pOVERLAP"></param>
        private static void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP)
        {
            var operation = (WinDivertOperation)ThreadPoolBoundHandle.GetNativeOverlappedState(pOVERLAP)!;
            if (errorCode > 0)
            {
                var exception = new Win32Exception((int)errorCode);
                operation.taskSource.SetException(exception);
            }
            else
            {
                operation.taskSource.SetResult((int)numBytes);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            this.boundHandle.FreeNativeOverlapped(this.nativeOverlapped);
        }


        int IValueTaskSource<int>.GetResult(short token)
        {
            return this.taskSource.GetResult(token);
        }

        ValueTaskSourceStatus IValueTaskSource<int>.GetStatus(short token)
        {
            return this.taskSource.GetStatus(token);
        }

        void IValueTaskSource<int>.OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            this.taskSource.OnCompleted(continuation, state, token, flags);
        }
    }
}
