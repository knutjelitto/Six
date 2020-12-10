using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ProtocolCompositionType : ItemList<IType>, IType
        {
            public ProtocolCompositionType(List<IType> types) : base(types) { }
            public ProtocolCompositionType() { }

            public static ProtocolCompositionType Parse(Parser parser)
            {
                var types = new List<IType>();
                do
                {
                    var type = IType.Parse(parser);
                    types.Add(type);
                }
                while (parser.Match(ToKind.Amper));

                return new ProtocolCompositionType(types);
            }

            public static ProtocolCompositionType Parse(Parser parser, IType first)
            {
                var types = new List<IType>();
                types.Add(first);
                do
                {
                    parser.Consume(ToKind.Amper);
                    var type = IType.Parse(parser);
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
}