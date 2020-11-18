namespace SixComp.ParseTree
{
    public class RequirementClause
    {
        public RequirementClause(RequirementList requirements)
        {
            Requirements = requirements;
        }

        public RequirementList Requirements { get; }

        public static RequirementClause Parse(Parser parser)
        {
            parser.Consume(ToKind.KwWhere);
            var requirements = RequirementList.Parse(parser);

            return new RequirementClause(requirements);
        }
    }
}
