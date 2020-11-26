using SixComp.Support;

namespace SixComp.ParseTree
{
    public class EnumCaseItem : AnyDeclaration
    {
        public EnumCaseItem(Name name, LabeledTypeList? types, Initializer? initializer)
        {
            Name = name;
            Types = types;
            Initializer = initializer;
        }

        public Name Name { get; }
        public LabeledTypeList? Types { get; }
        public Initializer? Initializer { get; }

        public static EnumCaseItem Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            LabeledTypeList? types = null;
            Initializer? initializer = null;
            if (parser.Current == ToKind.LParent)
            {
                types = LabeledTypeList.Parse(parser);
            }
            else if (parser.Current == ToKind.Assign)
            {
                initializer = Initializer.Parse(parser);
            }

            return new EnumCaseItem(name, types, initializer);
        }

        public void Write(IWriter writer)
        {
            var types = Types?.ToString() ?? string.Empty;

            writer.WriteLine($"case {Name}{types}");
        }
    }
}
