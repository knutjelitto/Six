using SixComp.Support;

namespace SixComp.Tree
{
    public class FunctionDeclaration : AnyDeclaration
    {
        public FunctionDeclaration(Prefix prefix, BaseName name, GenericParameterClause generics, ParameterClause parameters, FunctionResult? result, RequirementClause requirements, CodeBlock? block)
        {
            Prefix = prefix;
            Name = name;
            GenericParameters = generics;
            Parameters = parameters;
            Result = result;
            Requirements = requirements;
            Block = block;
        }

        public Prefix Prefix { get; }
        public BaseName Name { get; }
        public GenericParameterClause GenericParameters { get; }
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
            writer.WriteLine($"{Prefix}{Name}{GenericParameters}{Parameters}{Requirements}{Result}");
            Block?.Write(writer);
        }

        public override string ToString()
        {
            return $"{Prefix}{Name}{GenericParameters}{Parameters}{Requirements}{Result}{Block}";
        }
    }
}
