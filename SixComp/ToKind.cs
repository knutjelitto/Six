using SixComp.Support;
using System;

namespace SixComp
{
    [Flags]
    public enum ToClass
    {
        None = 0,
        Keyword = 1,
    }

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

        [Rep("(")]      LParent,
        [Rep(")")]      RParent,
        [Rep("{")]      LBrace,
        [Rep("}")]      RBrace,
        [Rep("[")]      LBracket,
        [Rep("]")]      RBracket,

        [Rep("=")]      Equal,
        [Rep("==")]     EqualEqual,
        [Rep("!")]      Bang,
        [Rep("!=")]     BangEqual,
        [Rep("?")]      Quest,
        [Rep("^")]      Caret,
        [Rep("~")]      Tilde,
        [Rep("&")]      Amper,
        [Rep("&&")]     AmperAmper,
        [Rep("|")]      VBar,
        [Rep("||")]     VBarVBar,

        [Rep("<")]      Less,
        [Rep("<=")]     LessEqual,
        [Rep(">")]      Greater,
        [Rep(">=")]     GreaterEqual,
        [Rep("->")]     MinusGreater,

        [Rep("+")]      Plus,
        [Rep("+=")]     PlusEqual,
        [Rep("-")]      Minus,
        [Rep("-=")]     MinusEqual,
        [Rep("*")]      Asterisk,
        [Rep("*=")]     AsteriskEqual,
        [Rep("/")]      Slash,
        [Rep("/=")]     SlashEqual,
        [Rep("%")]      Percent,

        [Rep("Any",         ToClass.Keyword)] KwANY,
        [Rep("break",       ToClass.Keyword)] KwBreak,
        [Rep("case",        ToClass.Keyword)] KwCase,
        [Rep("class",       ToClass.Keyword)] KwClass,
        [Rep("continue",    ToClass.Keyword)] KwContinue,
        [Rep("default",     ToClass.Keyword)] KwDefault,
        [Rep("else",        ToClass.Keyword)] KwElse,
        [Rep("enum",        ToClass.Keyword)] KwEnum,
        [Rep("extension",   ToClass.Keyword)] KwExtension,
        [Rep("false",       ToClass.Keyword)] KwFalse,
        [Rep("func",        ToClass.Keyword)] KwFunc,
        [Rep("get",         ToClass.Keyword)] KwGet,
        [Rep("guard",       ToClass.Keyword)] KwGuard,
        [Rep("if",          ToClass.Keyword)] KwIf,
        [Rep("import",      ToClass.Keyword)] KwImport,
        [Rep("init",        ToClass.Keyword)] KwInit,
        [Rep("inout",       ToClass.Keyword)] KwInout,
        [Rep("internal",    ToClass.Keyword)] KwInternal,
        [Rep("let",         ToClass.Keyword)] KwLet,
        [Rep("nil",         ToClass.Keyword)] KwNil,
        [Rep("nonmutating", ToClass.Keyword)] KwNonmutating,
        [Rep("mutating",    ToClass.Keyword)] KwMutating,
        [Rep("private",     ToClass.Keyword)] KwPrivate,
        [Rep("protocol",    ToClass.Keyword)] KwProtocol,
        [Rep("public",      ToClass.Keyword)] KwPublic,
        [Rep("rethrows",    ToClass.Keyword)] KwRethrows,
        [Rep("return",      ToClass.Keyword)] KwReturn,
        [Rep("self",        ToClass.Keyword)] KwSelf,
        [Rep("Self",        ToClass.Keyword)] KwSELF,
        [Rep("set",         ToClass.Keyword)] KwSet,
        [Rep("some",        ToClass.Keyword)] KwSome,
        [Rep("static",      ToClass.Keyword)] KwStatic,
        [Rep("struct",      ToClass.Keyword)] KwStruct,
        [Rep("super",       ToClass.Keyword)] KwSuper,
        [Rep("switch",      ToClass.Keyword)] KwSwitch,
        [Rep("throws",      ToClass.Keyword)] KwThrows,
        [Rep("true",        ToClass.Keyword)] KwTrue,
        [Rep("typealias",   ToClass.Keyword)] KwTypealias,
        [Rep("var",         ToClass.Keyword)] KwVar,
        [Rep("where",       ToClass.Keyword)] KwWhere,
        [Rep("while",       ToClass.Keyword)] KwWhile,

        [Rep("#if",         ToClass.Keyword)] CdIf,
        [Rep("#elseif",     ToClass.Keyword)] CdElseif,
        [Rep("#else",       ToClass.Keyword)] CdElse,
        [Rep("#endif",      ToClass.Keyword)] CdEndif,

        _LAST_,

        [Rep("<-eof->")]    EOF,
        [Rep("<-error->")]  ERROR,

        _FIN_,
    }
}
