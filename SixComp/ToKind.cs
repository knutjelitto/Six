namespace SixComp
{
    public enum ToKind
    {
        EOF,
        ERROR,

        Name,
        Number,
        String,

        Dot,
        Semi,
        Colon,
        Comma,

        LParen,
        RParent,
        LBrace,
        RBrace,
        LBrack,
        RBrack,

        Assign,
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
