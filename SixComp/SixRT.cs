using System;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable IDE0049 // Simplify Names

namespace SixComp
{
    public static class SixRT
    {
        const string DLL = "../../../../x64/Debug/SixRT.dll";
        const CharSet CS = CharSet.Ansi;
        const CallingConvention CC = CallingConvention.StdCall;

        public struct RtString
        {
            public IntPtr memory;
            public Int32 alloced;
            public Int32 used;
        }

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern void RtPrint(ref RtString s);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern ref RtString RtStringCreate(ref byte bytes, Int32 length);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern void RtStringFree(ref RtString s);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern ref RtString RtStringConcat(ref byte bytes, Int32 length);


        public static void PlayCheck()
        {
            var greating = "Hello RT";
            var bytes = Encoding.UTF8.GetBytes(greating);

            var s = RtStringCreate(ref bytes[0], bytes.Length);
            RtPrint(ref s);
        }
    }
}
