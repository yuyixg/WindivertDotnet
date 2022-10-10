﻿using System.Threading;

namespace WindivertDotnet
{
    sealed class WindivertSendOperation : WindivertOperation
    {
        private readonly WinDivertHandle handle;
        private readonly WinDivertPacket packet;

        public unsafe WindivertSendOperation(
            WinDivertHandle handle,
            WinDivertPacket packet,
            ThreadPoolBoundHandle boundHandle) : base(boundHandle)
        {
            this.handle = handle;
            this.packet = packet;
        }

        public unsafe override void IOControl(ref WinDivertAddress addr)
        {
            var length = 0;
            var addrLength = sizeof(WinDivertAddress);
            var flag = WinDivertNative.WinDivertSendEx(this.handle, this.packet, this.packet.Length, ref length, 0, ref addr, addrLength, this.NativeOverlapped);

            if (flag == true)
            {
                this.SetResult(length);
            }
        }
    }
}
