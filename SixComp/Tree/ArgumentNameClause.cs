using SixComp.Support;

namespace SixComp.Tree
{
    public class ArgumentNameClause
    {
        public ArgumentNameClause(ArgumentNameList names)
        {
            Names = names;
        }

        public ArgumentNameClause() 
            : this(new ArgumentNameList())
        {
        }

        public ArgumentNameList Names { get; }
        public bool Missing => Names.Missing;

        public static ArgumentNameClause? TryParse(Parser parser)
        {
            var offset = parser.Offset;

            if (parser.Match(ToKind.LParent))
            {
                var names = ArgumentNameList.TryParse(parser, new TokenSet(ToKind.RParent));
                if (names == null)
                {
                    parser.Offset = offset;
                    return null;
                }
                parser.Consume(ToKind.RParent);
                return new ArgumentNameClause(names);
            }

            return null;
        }

        public override string ToString()
        {
            return Missing ? string.Empty : $"({Names})";
        }
    }
}
