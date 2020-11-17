using SixComp.Support;

namespace SixComp.ParseTree
{
    public abstract class StructureType : AnyDeclaration
    {
        public StructureType((Name name, GenericParameterList parameters, DeclarationList declarations, TypeInheritanceClause inheritance) args)
        {
            Name = args.name;
            Parameters = args.parameters;
            Declarations = args.declarations;
            Inheritance = args.inheritance;
        }

        public Name Name { get; }
        public GenericParameterList Parameters { get; }
        public DeclarationList Declarations { get; }
        public TypeInheritanceClause Inheritance { get; }

        public static (Name, GenericParameterList, DeclarationList, TypeInheritanceClause) Parse(Parser parser, ToKind kind)
        {
            parser.Consume(kind);
            var name = Name.Parse(parser);
            var generics = parser.TryList(ToKind.Less, GenericParameterList.Parse);
            var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
            parser.Consume(ToKind.LBrace);
            var declarations = DeclarationList.Parse(parser);
            parser.Consume(ToKind.RBrace);

            return (name, generics, declarations, inheritance);
        }

        public void Write(IWriter writer, string keyword)
        {
            writer.WriteLine($"{keyword} {Name}{Parameters}");
            using (writer.Block())
            {
                Declarations.Write(writer);
            }
        }

        public abstract void Write(IWriter writer);
    }
}
