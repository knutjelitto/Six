using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathForcePostfix : IKeyPathPostfix
        {
            public KeyPathForcePostfix()
            {
            }
            public static KeyPathForcePostfix Parse(Parser parser)
            {
                parser.Consume(ToKind.Bang);
                return new KeyPathForcePostfix();
            }
            public override string ToString()
            {
                return $"{ToKind.Bang.GetRep()}";
            }
        }
    }
}
