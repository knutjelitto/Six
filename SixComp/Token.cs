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

        public override bool Equals(object? obj)
        {
            return obj is Token other && other.Kind == Kind;
        }

        public override int GetHashCode()
        {
            return Kind.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Kind}('{Span}')";
        }
    }
}
