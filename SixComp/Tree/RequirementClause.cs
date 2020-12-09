using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class RequirementClause : SyntaxNode, IWritable
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwWhere);

            public RequirementClause(RequirementList requirements)
            {
                Requirements = requirements;
            }

            public RequirementClause() : this(new RequirementList())
            {
            }

            public RequirementList Requirements { get; }

            public bool Missing => Requirements.Missing;

            public static RequirementClause Parse(Parser parser)
            {
                parser.Consume(ToKind.KwWhere);
                var requirements = RequirementList.Parse(parser);

                return new RequirementClause(requirements);
            }

            public override string ToString()
            {
                if (Missing)
                {
                    return string.Empty;
                }

                return $"where {Requirements}";
            }

            public void Write(IWriter writer)
            {
                if (!Missing)
                {
                    using (writer.Indent())
                    {
                        writer.WriteLine($"{Kw.Where} {Requirements}");
                    }
                }
            }
        }
    }
}