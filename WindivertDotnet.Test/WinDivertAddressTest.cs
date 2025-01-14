﻿using Xunit;

namespace WindivertDotnet.Test
{
    public class WinDivertAddressTest
    {
        [Fact]
        public void CloneTest()
        {
            using var addr = new WinDivertAddress();
            addr.Flags = WinDivertAddressFlag.Loopback;

            using var clone = addr.Clone();
            Assert.Equal(addr.Flags, clone.Flags);
        }
    }
}
