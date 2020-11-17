using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class TypeAnnotation : AnyType
    {
        private TypeAnnotation(AnyType type, bool inout)
        {
            Type = type;
            Inout = inout;
        }

        public AnyType Type { get; }
        public bool Inout { get; }

        public static TypeAnnotation Parse(Parser parser)
        {
            parser.Consume(ToKind.Colon);
            var inout = parser.Match(ToKind.KwInout);
            var type = AnyType.Parse(parser);

            return new TypeAnnotation(type, inout);
        }

        public override string ToString()
        {
            var inout = Inout ? "inout " : string.Empty;
            return $": {inout}{Type}";
        }
    }
}
