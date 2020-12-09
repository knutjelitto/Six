namespace SixComp
{
    public partial class Tree
    {
        public class Parameter
        {
            public Parameter(Prefix prefix, BaseName? @extern, BaseName intern, TypeAnnotation type, bool variadic, Initializer? initializer)
            {
                Prefix = prefix;
                Extern = @extern;
                Intern = intern;
                Type = type;
                Variadic = variadic;
                Initializer = initializer;
            }

            public Prefix Prefix { get; }
            public BaseName? Extern { get; }
            public BaseName Intern { get; }
            public TypeAnnotation Type { get; }
            public bool Variadic { get; }
            public Initializer? Initializer { get; }

            public static Parameter Parse(Parser parser)
            {
                var prefix = Prefix.Parse(parser, onlyAttributes: true);
                BaseName? @extern = null;
                if (parser.NextNext == ToKind.Colon)
                {
                    @extern = BaseName.Parse(parser);
                }
                var intern = BaseName.Parse(parser);
                var type = TypeAnnotation.Parse(parser);
                var variadic = parser.Match(ToKind.DotDotDot);
                var initializer = parser.Try(ToKind.Assign, Initializer.Parse);

                return new Parameter(prefix, @extern, intern, type, variadic, initializer);
            }

            public override string ToString()
            {
                var space = Extern == null ? string.Empty : " ";
                var variadic = Variadic ? "..." : string.Empty;
                return $"{Prefix}{Extern}{space}{Intern}{Type}{variadic}{Initializer}";
            }
        }
    }
}