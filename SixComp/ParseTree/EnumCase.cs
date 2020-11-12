using SixComp.Support;

namespace SixComp.ParseTree
{
    public class EnumCase : Declaration
    {
        public EnumCase(Token name, TupleType? types)
        {
            Name = name;
            Types = types;
        }

        public Token Name { get; }
        public TupleType? Types { get; }

        public override void Write(IWriter writer)
        {
            var types = Types?.ToString() ?? string.Empty;

            writer.WriteLine($"case {Name}{types}");
        }
    }
}
