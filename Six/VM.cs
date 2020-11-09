using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Six
{
    public static class VM
    {
        const string DLL = "VM.dll";
        const CharSet CS = CharSet.Ansi;
        const CallingConvention CC = CallingConvention.StdCall;


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long Execute(ref byte addr);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long GetStackAt(long index);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI8(ref byte addr, sbyte value);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI16(ref byte addr, short value);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI32(ref byte addr, int value);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI64(ref byte addr, long value);
        

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitAdd(ref byte addr);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitRet(ref byte addr);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long Add(long x, long y);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern void WriteMessage([MarshalAs(UnmanagedType.LPStr)] string message);
    }
}
