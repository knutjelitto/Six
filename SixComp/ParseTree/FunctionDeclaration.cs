using SixComp.Support;

namespace SixComp.ParseTree
{
    public class FunctionDeclaration : AnyDeclaration
    {
        public FunctionDeclaration(Name name, GenericParameterClause generics, ParameterClause parameters, FunctionResult? returns, RequirementClause requirements, CodeBlock? block)
        {
            Name = name;
            GenericParameters = generics;
            Parameters = parameters;
            Returns = returns;
            Requirements = requirements;
            Block = block;
        }

        public Name Name { get; }
        public GenericParameterClause GenericParameters { get; }
        public ParameterClause Parameters { get; }
        public FunctionResult? Returns { get; }
        public RequirementClause Requirements { get; }
        public CodeBlock? Block { get; }

        public static FunctionDeclaration Parse(Parser parser)
        {
            //TODO: is incomplete
            parser.Consume(ToKind.KwFunc);
            var name = Name.Parse(parser, withOperators: true);
            var genericParameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var parameters = ParameterClause.Parse(parser);
            var throws = parser.Match(ToKind.KwThrows);
            var rethrows = parser.Match(ToKind.KwRethrows);
            var returns = parser.Try(ToKind.MinusGreater, FunctionResult.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
            var block = parser.Try(ToKind.LBrace, CodeBlock.Parse);

            return new FunctionDeclaration(name, genericParameters, parameters, returns, requirements, block);
        }

        public void Write(IWriter writer)
        {
            var returns = Returns == null ? string.Empty : $" -> {Returns}";
            writer.WriteLine($"func {Name}{GenericParameters}{Parameters}{returns}");
            Block?.Write(writer);
        }
    }
}
