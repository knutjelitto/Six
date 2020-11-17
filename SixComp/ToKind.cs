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

        KwANY,
        KwBreak,
        KwCase,
        KwClass,
        KwContinue,
        KwDefault,
        KwElse,
        KwEnum,
        KwExtension,
        KwFunc,
        KwIf,
        KwImport,
        KwInit,
        KwLet,
        KwNil,
        KwProtocol,
        KwPublic,
        KwReturn,
        KwSelf,
        KwSELF,
        KwStruct,
        KwSuper,
        KwSwitch,
        KwTypealias,
        KwVar,
        KwWhere,

        _FIN_,
    }
}
