using System;

namespace SixComp
{
    [Flags]
    public enum ToKind
    {
        EOF,
        ERROR,

        Name,
        Number,
        String,

        Dot,
        DotDotDot,
        DotDotLess,
        Semi,
        Colon,
        Comma,

        LParen,
        RParent,
        LBrace,
        RBrace,
        LBrack,
        RBrack,

        Equal,
        Bang,
        Quest,
        Caret,
        Tilde,

        Less,
        Greater,

        Plus,
        Minus,
        Asterisk,
        Slash,
        Percent,

        KwLet,
        KwVar,
        KwFunc,
        KwClass,
        KwStruct,
        KwEnum,
        KwCase,
        KwReturn,
    }
}
