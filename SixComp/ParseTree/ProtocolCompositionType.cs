using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ProtocolCompositionType : ItemList<AnyType>
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
    }
}
