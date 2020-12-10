namespace SixComp
{
    public partial class ParseTree
    {
        public class TuplePatternElement
        {
            public TuplePatternElement(NameLabel? name, IPattern pattern)
            {
                Name = name;
                Pattern = pattern;
            }

            public NameLabel? Name { get; }
            public IPattern Pattern { get; }

            public static TuplePatternElement Parse(Parser parser)
            {
                var name = (NameLabel?)null;

                if (BaseName.CanParse(parser) && parser.Next == ToKind.Colon)
                {
                    name = NameLabel.Parse(parser);
                }

                var pattern = IPattern.Parse(parser);

                return new TuplePatternElement(name, pattern);
            }

            public override string ToString()
            {
                return $"{Name}{Pattern}";
            }
        }
    }
}