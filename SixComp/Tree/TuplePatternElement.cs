namespace SixComp
{
    public partial class Tree
    {
        public class TuplePatternElement
        {
            public TuplePatternElement(NameLabel? name, AnyPattern pattern)
            {
                Name = name;
                Pattern = pattern;
            }

            public NameLabel? Name { get; }
            public AnyPattern Pattern { get; }

            public static TuplePatternElement Parse(Parser parser)
            {
                var name = (NameLabel?)null;

                if (BaseName.CanParse(parser) && parser.Next == ToKind.Colon)
                {
                    name = NameLabel.Parse(parser);
                }

                var pattern = AnyPattern.Parse(parser);

                return new TuplePatternElement(name, pattern);
            }

            public override string ToString()
            {
                return $"{Name}{Pattern}";
            }
        }
    }
}