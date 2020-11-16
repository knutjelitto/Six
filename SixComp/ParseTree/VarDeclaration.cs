using SixComp.Support;

namespace SixComp.ParseTree
{
    public class VarDeclaration : AnyDeclaration
    {
        public VarDeclaration(Name name, AnyType? type, AnyExpression? initializer)
        {
            Name = name;
            Type = type;
            Initializer = initializer;
        }

        public Name Name { get; }
        public AnyType? Type { get; }
        public AnyExpression? Initializer { get; }

        public static VarDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwVar);
            var name = Name.Parse(parser);
            var type = parser.TryMatch(ToKind.Colon, AnyType.Parse);
            var init = parser.TryMatch(ToKind.Equal, AnyExpression.Parse);

            return new VarDeclaration(name, type, init);
        }

        public void Write(IWriter writer)
        {
            var type = Type == null ? string.Empty : $": {Type}";
            var init = Initializer == null ? string.Empty : $" = {Initializer}";

            writer.Write($"var {Name}{type}{init}");
            writer.WriteLine();
        }
    }
}
