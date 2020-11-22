﻿using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class RequirementList : ItemList<AnyRequirement>
    {
        public RequirementList(List<AnyRequirement> requirements) : base(requirements) { }
        public RequirementList() { }

        public static RequirementList Parse(Parser parser)
        {
            var requirements = new List<AnyRequirement>();

            do
            {
                var requirement = AnyRequirement.Parse(parser);
                requirements.Add(requirement);
            }
            while (parser.Match(ToKind.Comma));

            return new RequirementList(requirements);
        }
    }
}
