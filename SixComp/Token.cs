using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp
{
    public struct Token
    {
        public Token(Span span, ToKind kind)
        {
            Span = span;
            Kind = kind;
        }

        public Span Span { get; }
        public ToKind Kind { get; }
    }
}
