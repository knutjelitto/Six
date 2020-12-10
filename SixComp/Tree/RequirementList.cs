using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class RequirementList : ItemList<ITypeRestriction>
        {
            public RequirementList(List<ITypeRestriction> requirements) : base(requirements) { }
            public RequirementList() { }

            public static RequirementList Parse(Parser parser)
            {
                var requirements = new List<ITypeRestriction>();

                do
                {
                    var requirement = ITypeRestriction.Parse(parser);
                    requirements.Add(requirement);
                }
                while (parser.Match(ToKind.Comma));

                return new RequirementList(requirements);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}