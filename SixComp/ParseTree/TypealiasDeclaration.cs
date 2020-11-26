﻿using SixComp.Support;

namespace SixComp.ParseTree
{
    public class TypealiasDeclaration : AnyDeclaration
    {
        public TypealiasDeclaration(Prefix prefix, Name name, GenericParameterClause parameters, TypealiasAssignment assignment, RequirementClause requirements)
        {
            Prefix = prefix;
            Name = name;
            Parameters = parameters;
            Assignment = assignment;
            Requirements = requirements;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public GenericParameterClause Parameters { get; }
        public TypealiasAssignment Assignment { get; }
        public RequirementClause Requirements { get; }

        public static TypealiasDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwTypealias);

            var name = Name.Parse(parser);
            var parameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var assignment = TypealiasAssignment.Parse(parser);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);

            return new TypealiasDeclaration(prefix, name, parameters, assignment, requirements);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"typealias {Name}{Parameters}{Assignment}");
            Requirements.Write(writer);
        }
    }
}
