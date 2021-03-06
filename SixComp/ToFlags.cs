﻿using System;

namespace SixComp
{
    [Flags]
    public enum ToFlags
    {
        None = 0,

        Operator = 1 << 0,
        PrefixOperator = 1 << 1,
        PostfixOperator = 1 << 2,
        InfixOperator = 1 << 3,

        AnyOperator = Operator | PrefixOperator | PostfixOperator | InfixOperator,

        Identifier = 1 << 4,
        Keyword = 1 << 5,
        Special = 1 << 6,

        /// <summary>
        /// At '<' splitable token
        /// </summary>
        OpSplitLess = 1 << 7,
        /// <summary>
        /// At '>' splitable token
        /// </summary>
        OpSplitGreater = 1 << 8,
        /// <summary>
        /// At '?' splitable token
        /// </summary>
        OpSplitQuest = 1 << 9,
        /// <summary>
        /// At '!' splitable token
        /// </summary>
        OpSplitBang = 1 << 10,

        /// <summary>
        /// Token is considered as whitespace before operator
        /// </summary>
        OpSpaceBefore = 1 << 11,
        /// <summary>
        /// Token is considered as whitespace after operator
        /// </summary>
        OpSpaceAfter = 1 << 12,
        /// <summary>
        /// Token is considered as whitespace before or after operator
        /// </summary>
        OpSpaceAny = OpSpaceBefore | OpSpaceAfter,

        Implicit = 1 << 13,
    }
}
