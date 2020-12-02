using SixComp.Support;

namespace SixComp.Tree
{
    public class TypeInheritanceClause
    {
        public static TokenSet Firsts = new TokenSet(ToKind.Colon);

        public TypeInheritanceClause(TypeInheritanceList types)
        {
            Types = types;
        }

        public TypeInheritanceClause()
            : this(new TypeInheritanceList())
        {
        }

        public TypeInheritanceList Types { get; }

        public static TypeInheritanceClause Parse(Parser parser)
        {
            parser.Consume(ToKind.Colon);

            var types = TypeInheritanceList.Parse(parser);

            return new TypeInheritanceClause(types);
        }

        public override string ToString()
        {
            return Types.Missing ? string.Empty : $": {Types}";
        }
    }
}
