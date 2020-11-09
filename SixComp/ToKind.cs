using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp
{
    public enum ToKind
    {
        Identifier,
        Number,
        String,

        Dot,
        Semi,
        Colon,

        LParen,
        RParent,
        LBrace,
        RBrace,
        LBrack,
        RBrack,

        OpPlus,
        OpMinus,
        OpMul,
        OpDiv,

        KwLet,
        KwVar,
        KwFunc,
        KwReturn,
    }
}
