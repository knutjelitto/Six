using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ProtocolCompositionType : ItemList<AnyType>, AnyType
    {
        public ProtocolCompositionType(List<AnyType> types) : base(types) { }
        public ProtocolCompositionType() { }

        public static ProtocolCompositionType Parse(Parser parser)
        {
            var types = new List<AnyType>();
            do
            {
                var type = AnyType.Parse(parser);
                types.Add(type);
            }
            while (parser.Match(ToKind.Amper));

            return new ProtocolCompositionType(types);
        }

        public static ProtocolCompositionType Parse(Parser parser, AnyType first)
        {
            var types = new List<AnyType>();
            types.Add(first);
            do
            {
                parser.Consume(ToKind.Amper);
                var type = AnyType.Parse(parser);
                types.Add(type);
            }
            while (parser.IsInfixOperator() && parser.Current == ToKind.Amper);

            return new ProtocolCompositionType(types);
        }

        public override string ToString()
        {
            return string.Join(" & ", this);
        }
    }
}
