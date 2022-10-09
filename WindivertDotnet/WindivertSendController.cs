﻿using System.Runtime.InteropServices;
using System.Threading;

namespace WindivertDotnet
{
    sealed class WindivertSendController : WindivertController
    {
        private readonly WinDivertHandle handle;
        private readonly WinDivertPacket packet;

        public unsafe WindivertSendController(
            WinDivertHandle handle,
            WinDivertPacket packet,
            ThreadPoolBoundHandle boundHandle,
            IOCompletionCallback completionCallback) : base(boundHandle, completionCallback)
        {
            this.handle = handle;
            this.packet = packet;
        }

        public unsafe override void IoControl(ref WinDivertAddress addr)
        {
            var length = 0;
            var addrLength = sizeof(WinDivertAddress);
            var flag = WinDivertNative.WinDivertSendEx(this.handle, this.packet, this.packet.Capacity, ref length, 0, ref addr, addrLength, this.NativeOverlapped);

            if (flag == false)
            {
                var errorCode = Marshal.GetLastWin32Error();
                if (errorCode != ERROR_IO_PENDING)
                {
                    this.SetException(errorCode);
                }
            }
            else
            {
                this.SetResult(length);
            }
        }
    }
}
