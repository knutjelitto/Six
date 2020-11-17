using SixComp.Support;
using System;

namespace SixComp
{
    [Flags]
    public enum ToKind
    {
        Name,
        Number,
        String,

        Dot,
        DotDotDot,
        DotDotLess,
        Semi,
        Colon,
        Comma,

        [Rep("@")]      At,

        LParent,
        RParent,
        [Rep("{")]      LBrace,
        [Rep("}")]      RBrace,
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
        KwFalse,
        KwFunc,
        KwGet,
        KwIf,
        KwImport,
        KwInit,
        KwInout,
        KwLet,
        KwNil,
        KwNonmutating,
        KwMutating,
        KwProtocol,
        KwPublic,
        [Rep("return")]     KwReturn,
        KwSelf,
        KwSELF,
        KwSet,
        KwStruct,
        KwSuper,
        KwSwitch,
        KwTrue,
        KwTypealias,
        KwVar,
        KwWhere,

        _LAST_,

        [Rep("<-eof->")]    EOF,
        [Rep("<-error->")]  ERROR,

        _FIN_,
    }
}
