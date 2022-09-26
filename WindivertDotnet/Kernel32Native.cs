﻿using System;
using System.Runtime.InteropServices;

namespace WindivertDotnet
{
    static class Kernel32Native
    {
        private const string library = "kernel32.dll";

        [DllImport(library, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
    }
}
