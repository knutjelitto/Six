using System;

namespace Six
{
    public class Ins
    {
        public struct u32
        {
            readonly UInt32 value;
            public u32(uint value) => this.value = value;
        }

        public struct u64
        {
            readonly UInt64 value;
            public u64(ulong value) => this.value = value;
        }
    }
}
