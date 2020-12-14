using Six.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class FunctionDeclaration : IDeclaration, INominal
        {
            public FunctionDeclaration(Prefix prefix, BaseName name, GenericParameterClause generics, ParameterClause parameters, FunctionResult? result, RequirementClause requirements, CodeBlock? block)
            {
                Prefix = prefix;
                Name = name;
                Generics = generics;
                Parameters = parameters;
                Result = result;
                Requirements = requirements;
                Block = block;
            }

            public Prefix Prefix { get; }
            public BaseName Name { get; }
            public GenericParameterClause Generics { get; }
            public ParameterClause Parameters { get; }
            public FunctionResult? Result { get; }
            public RequirementClause Requirements { get; }
            public CodeBlock? Block { get; }

            public static FunctionDeclaration Parse(Parser parser, Prefix prefix)
            {
                // already parsed //parser.Consume(ToKind.KwFunc);
                var name = BaseName.Parse(parser, withOperators: true);
                var genericParameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
                var parameters = ParameterClause.Parse(parser);
                var async = parser.Match(ToKind.KwAsync);
                var throws = parser.Match(ToKind.KwThrows);
                var rethrows = parser.Match(ToKind.KwRethrows);
                var returns = parser.Try(ToKind.Arrow, FunctionResult.Parse);
                var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
                var block = parser.Try(ToKind.LBrace, CodeBlock.Parse);

                return new FunctionDeclaration(prefix, name, genericParameters, parameters, returns, requirements, block);
            }

            public void Write(IWriter writer)
            {
                Prefix.Write(writer);
                writer.WriteLine($"{Name}{Generics}{Parameters}{Requirements}{Result}");
                Block?.Write(writer);
            }

            public override string ToString()
            {
                return $"{Prefix}{Name}{Generics}{Parameters}{Requirements}{Result}{Block}";
            }
        }
    }
}