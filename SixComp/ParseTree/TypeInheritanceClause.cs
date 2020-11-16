namespace SixComp.ParseTree
{
    public class TypeInheritanceClause
    {
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
