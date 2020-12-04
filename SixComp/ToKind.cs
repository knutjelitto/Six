﻿using SixComp.Support;
using System;

namespace SixComp
{
    [Flags]
    public enum ToClass
    {
        None = 0,
        Keyword = 1,
        Operator = 2,
    }

    public enum ToKind
    {
        Name,
        Number,
        String,

        [Rep(";")] SemiColon,
        [Rep(":")] Colon,
        [Rep(",")] Comma,

        [Rep(".", ToClass.Operator)] Dot,
        [Rep("->", ToClass.Operator)] Arrow,
        [Rep("!", ToClass.Operator)] Bang,
        [Rep("?", ToClass.Operator)] Quest,
        [Rep(">", ToClass.Operator)] Greater,
        [Rep("<", ToClass.Operator)] Less,
        [Rep("=", ToClass.Operator)] Assign,
        [Rep("&", ToClass.Operator)] Amper,
        [Rep("==", ToClass.Operator)] Equals,
        [Rep("<-op->", ToClass.Operator)] Operator,

        [Rep("@")] At,
        [Rep("\\")] Backslash,

        [Rep("(")] LParent,
        [Rep(")")] RParent,
        [Rep("{")] LBrace,
        [Rep("}")] RBrace,
        [Rep("[")] LBracket,
        [Rep("]")] RBracket,

        [Rep("...", ToClass.Operator)] DotDotDot,
        [Rep("..<", ToClass.Operator)] DotDotLess,

        [Rep("!=", ToClass.Operator)] BangEqual,
        [Rep("^", ToClass.Operator)] Caret,
        [Rep("~", ToClass.Operator)] Tilde,
        [Rep("&&", ToClass.Operator)] AmperAmper,
        [Rep("|", ToClass.Operator)] VBar,
        [Rep("||", ToClass.Operator)] VBarVBar,

        [Rep("<<", ToClass.Operator)] LessLess,
        [Rep("<<<", ToClass.Operator)] LessLessLess,
        [Rep("<=", ToClass.Operator)] LessEqual,
        [Rep(">=", ToClass.Operator)] GreaterEqual,

        [Rep("+", ToClass.Operator)] Plus,
        [Rep("&+", ToClass.Operator)] AmperPlus,
        [Rep("+=", ToClass.Operator)] PlusEqual,
        [Rep("-", ToClass.Operator)] Minus,
        [Rep("&-", ToClass.Operator)] AmperMinus,
        [Rep("-=", ToClass.Operator)] MinusEqual,
        [Rep("*", ToClass.Operator)] Asterisk,
        [Rep("&*", ToClass.Operator)] AmperAsterisk,
        [Rep("*=", ToClass.Operator)] AsteriskEqual,
        [Rep("/", ToClass.Operator)] Slash,
        [Rep("&/", ToClass.Operator)] AmperSlash,
        [Rep("/=", ToClass.Operator)] SlashEqual,
        [Rep("%", ToClass.Operator)] Percent,
        [Rep("&%", ToClass.Operator)] AmperPercent,
        [Rep("%=", ToClass.Operator)] PercentEqual,

        [Rep("??", ToClass.Operator)] QuestQuest,

        [Rep("is", ToClass.Operator | ToClass.Keyword)] KwIs,
        [Rep("as", ToClass.Operator | ToClass.Keyword)] KwAs,
        [Rep("as!", ToClass.Operator | ToClass.Keyword)] KwAsForce,
        [Rep("as?", ToClass.Operator | ToClass.Keyword)] KwAsChain,

        [Rep("Any", ToClass.Keyword)] KwANY,
        [Rep("associatedtype", ToClass.Keyword)] KwAssociatedType,
        [Rep("async", ToClass.Keyword)] KwAsync,
        [Rep("break", ToClass.Keyword)] KwBreak,
        [Rep("case", ToClass.Keyword)] KwCase,
        [Rep("class", ToClass.Keyword)] KwClass,
        [Rep("continue", ToClass.Keyword)] KwContinue,
        [Rep("convenience", ToClass.Keyword)] KwConvenience,
        [Rep("default", ToClass.Keyword)] KwDefault,
        [Rep("defer", ToClass.Keyword)] KwDefer,
        [Rep("deinit", ToClass.Keyword)] KwDeinit,
        [Rep("didSet", ToClass.Keyword)] KwDidSet,
        [Rep("dynamic", ToClass.Keyword)] KwDynamic,
        [Rep("else", ToClass.Keyword)] KwElse,
        [Rep("enum", ToClass.Keyword)] KwEnum,
        [Rep("extension", ToClass.Keyword)] KwExtension,
        [Rep("false", ToClass.Keyword)] KwFalse,
        [Rep("fileprivate", ToClass.Keyword)] KwFileprivate,
        [Rep("final", ToClass.Keyword)] KwFinal,
        [Rep("for", ToClass.Keyword)] KwFor,
        [Rep("func", ToClass.Keyword)] KwFunc,
        [Rep("get", ToClass.Keyword)] KwGet,
        [Rep("guard", ToClass.Keyword)] KwGuard,
        [Rep("if", ToClass.Keyword)] KwIf,
        [Rep("import", ToClass.Keyword)] KwImport,
        [Rep("in", ToClass.Keyword)] KwIn,
        [Rep("indirect", ToClass.Keyword)] KwIndirect,
        [Rep("infix", ToClass.Keyword)] KwInfix,
        [Rep("init", ToClass.Keyword)] KwInit,
        [Rep("inout", ToClass.Keyword)] KwInout,
        [Rep("internal", ToClass.Keyword)] KwInternal,
        [Rep("lazy", ToClass.Keyword)] KwLazy,
        [Rep("let", ToClass.Keyword)] KwLet,
        [Rep("mutating", ToClass.Keyword)] KwMutating,
        [Rep("nil", ToClass.Keyword)] KwNil,
        [Rep("nonmutating", ToClass.Keyword)] KwNonmutating,
        [Rep("open", ToClass.Keyword)] KwOpen,
        [Rep("operator", ToClass.Keyword)] KwOperator,
        [Rep("optional", ToClass.Keyword)] KwOptional,
        [Rep("override", ToClass.Keyword)] KwOverride,
        [Rep("postfix", ToClass.Keyword)] KwPostfix,
        [Rep("prefix", ToClass.Keyword)] KwPrefix,
        [Rep("private", ToClass.Keyword)] KwPrivate,
        [Rep("protocol", ToClass.Keyword)] KwProtocol,
        [Rep("public", ToClass.Keyword)] KwPublic,
        [Rep(Kw.Repeat, ToClass.Keyword)] KwRepeat,
        [Rep("required", ToClass.Keyword)] KwRequired,
        [Rep("rethrows", ToClass.Keyword)] KwRethrows,
        [Rep("return", ToClass.Keyword)] KwReturn,
        [Rep("safe", ToClass.Keyword)] KwSafe,
        [Rep("self", ToClass.Keyword)] KwSelf,
        [Rep("Self", ToClass.Keyword)] KwSELF,
        [Rep("set", ToClass.Keyword)] KwSet,
        [Rep("some", ToClass.Keyword)] KwSome,
        [Rep("static", ToClass.Keyword)] KwStatic,
        [Rep("struct", ToClass.Keyword)] KwStruct,
        [Rep("subscript", ToClass.Keyword)] KwSubscript,
        [Rep("super", ToClass.Keyword)] KwSuper,
        [Rep("switch", ToClass.Keyword)] KwSwitch,
        [Rep("throws", ToClass.Keyword)] KwThrows,
        [Rep("true", ToClass.Keyword)] KwTrue,
        [Rep("try", ToClass.Keyword)] KwTry,
        [Rep("typealias", ToClass.Keyword)] KwTypealias,
        [Rep("unowned", ToClass.Keyword)] KwUnowned,
        [Rep("unsafe", ToClass.Keyword)] KwUnsafe,
        [Rep("var", ToClass.Keyword)] KwVar,
        [Rep("weak", ToClass.Keyword)] KwWeak,
        [Rep(Kw.Where, ToClass.Keyword)] KwWhere,
        [Rep(Kw.While, ToClass.Keyword)] KwWhile,
        [Rep("willSet", ToClass.Keyword)] KwWillSet,
        [Rep("yield", ToClass.Keyword)] KwYield,

        [Rep("__consuming", ToClass.Keyword)] Kw__Consuming,
        [Rep("__owned", ToClass.Keyword)] Kw__Owned,
        [Rep("__shared", ToClass.Keyword)] Kw__Shared,


        [Rep("#if", ToClass.Keyword)] CdIf,
        [Rep("#elseif", ToClass.Keyword)] CdElseif,
        [Rep("#else", ToClass.Keyword)] CdElse,
        [Rep("#endif", ToClass.Keyword)] CdEndif,

        [Rep("#file", ToClass.Keyword)] CdFile,
        [Rep("#function", ToClass.Keyword)] CdFunction,
        [Rep("#line", ToClass.Keyword)] CdLine,
        [Rep("#column", ToClass.Keyword)] CdColumn,
        [Rep("#available", ToClass.Keyword)] CdAvailable,

        _LAST_,

        [Rep("<-eof->")] EOF,
        [Rep("<-error->")] ERROR,

        _FIN_,
    }
}
