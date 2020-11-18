using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ProtocolCompositionType : ItemList<TypeIdentifier>
    {
        public ProtocolCompositionType(List<TypeIdentifier> types) : base(types) { }
        public ProtocolCompositionType() { }

        public static ProtocolCompositionType Parse(Parser parser)
        {
            var types = new List<TypeIdentifier>();
            do
            {
                var type = TypeIdentifier.Parse(parser);
                types.Add(type);
            }
            while (parser.Match(ToKind.Amper));

            return new ProtocolCompositionType(types);
        }
    }
}
