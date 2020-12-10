using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class EnumCaseItem : IDeclaration
        {
            public EnumCaseItem(BaseName name, TupleType? types, Initializer? initializer)
            {
                Name = name;
                Tuple = types;
                Initializer = initializer;
            }

            public BaseName Name { get; }
            public TupleType? Tuple { get; }
            public Initializer? Initializer { get; }

            public static EnumCaseItem Parse(Parser parser)
            {
                var name = BaseName.Parse(parser);
                TupleType? types = null;
                Initializer? initializer = null;
                if (parser.Current == ToKind.LParent)
                {
                    types = TupleType.Parse(parser);
                }
                else if (parser.Current == ToKind.Assign)
                {
                    initializer = Initializer.Parse(parser);
                }

                return new EnumCaseItem(name, types, initializer);
            }

            public void Write(IWriter writer)
            {
                var types = Tuple?.ToString() ?? string.Empty;

                writer.WriteLine($"case {Name}{types}");
            }

            public override string ToString()
            {
                return $"{Name}{Tuple}{Initializer}";
            }
        }
    }
}