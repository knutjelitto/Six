using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class MetatypeType : AnyType
    {
        public AnyType Type { get; }
        public MetaTypeKind Kind { get; }

        public MetatypeType(AnyType type, MetaTypeKind kind)
        {
            Type = type;
            Kind = kind;
        }

        public enum MetaTypeKind
        {
            Type,
            Protocol,
        }

        public static MetatypeType Parse(Parser parser, AnyType type)
        {
            parser.Consume(ToKind.Dot);

            if (parser.Match(Contextual.Type))
            {
                return new MetatypeType(type, MetaTypeKind.Type);
            }
            if (parser.Match(Contextual.Protocol))
            {
                return new MetatypeType(type, MetaTypeKind.Protocol);
            }

            throw new InvalidOperationException($"{typeof(MetatypeType)} - {parser.CurrentToken}");
        }
    }
}
