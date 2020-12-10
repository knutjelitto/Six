namespace SixComp
{
    public partial class ParseTree
    {
        public class IdentifierPattern : SyntaxNode, IPattern
        {
            public IdentifierPattern(BaseName name)
            {
                Name = name;
            }

            public BaseName Name { get; }

            public static IdentifierPattern Parse(Parser parser)
            {
                var name = BaseName.Parse(parser);

                return new IdentifierPattern(name);
            }

            public bool NameOnly => true;

            public override string ToString()
            {
                return $"{Name}";
            }
        }
    }
}