using SixComp.Support;

namespace SixComp.ParseTree
{
    public class EnumCaseItem : AnyDeclaration
    {
        public EnumCaseItem(Name name, TupleType? types)
        {
            Name = name;
            Types = types;
        }

        public Name Name { get; }
        public TupleType? Types { get; }

        public static EnumCaseItem Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            var types = parser.Try(ToKind.LParen, TupleType.Parse);

            return new EnumCaseItem(name, types);
        }

        public void Write(IWriter writer)
        {
            var types = Types?.ToString() ?? string.Empty;

            writer.WriteLine($"case {Name}{types}");
        }
    }
}
