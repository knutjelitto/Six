using System;

namespace SixComp
{
    [Flags]
    public enum ToFlags
    {
        IndeterminateOperator = 1 << 0,
        PrefixOperator = 1 << 1,
        PostfixOperator = 1 << 2,
        InfixOperator = 1 << 3,

        AnyOperator = IndeterminateOperator | PrefixOperator | PostfixOperator | InfixOperator,

        Identifier = 1 << 4,
        Keyword = 1 << 5,

        Special = 1 << 6,
    }
}
