namespace SixComp
{
    public partial class Tree
    {
        public class IdentifierPattern : SyntaxNode, AnyPattern
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