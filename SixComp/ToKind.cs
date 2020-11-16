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
        LBracket,
        RBracket,

        Equal,
        Bang,
        Quest,
        Caret,
        Tilde,

        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        MinusGreater,

        Plus,
        Minus,
        Asterisk,
        Slash,
        Percent,

        KwBreak,
        KwCase,
        KwClass,
        KwContinue,
        KwElse,
        KwEnum,
        KwFunc,
        KwIf,
        KwInit,
        KwLet,
        KwProtocol,
        KwSelf,
        KwStruct,
        KwReturn,
        KwVar,
    }
}
