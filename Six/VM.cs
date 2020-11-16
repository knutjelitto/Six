using System.Runtime.InteropServices;

namespace Six
{
    public static class VM
    {
        const string DLL = "VM.dll";
        const CharSet CS = CharSet.Ansi;
        const CallingConvention CC = CallingConvention.StdCall;


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long Execute(long builder);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long GetStackAt(long index);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long CreateBuilder();

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern void DestroyBuilder(long builder);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI8(long addr, sbyte value);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI16(long addr, short value);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI32(long addr, int value);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitI64(long addr, long value);
        

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitAdd(long addr);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long EmitRet(long addr);


        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern long Add(long x, long y);

        [DllImport(DLL, CallingConvention = CC, CharSet = CS)]
        public static extern void WriteMessage([MarshalAs(UnmanagedType.LPStr)] string message);
    }
}
