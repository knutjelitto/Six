using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class ProtocolDeclaration : IWriteable
    {
        public ProtocolDeclaration(Name name, GenericParameterClause parameters, TypeInheritanceClause inheritanceClause, DeclarationList declarations)
        {
            Name = name;
            Parameters = parameters;
            InheritanceClause = inheritanceClause;
            Declarations = declarations;
        }

        public Name Name { get; }
        public GenericParameterClause Parameters { get; }
        public TypeInheritanceClause InheritanceClause { get; }
        public DeclarationList Declarations { get; }

        public static ProtocolDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwProtocol);
            var name = Name.Parse(parser);
            var parameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var inheritance = TypeInheritanceClause.Parse(parser);

            throw new NotImplementedException();
        }
    }
}
