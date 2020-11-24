using SixComp.Support;

namespace SixComp.ParseTree
{
    public class FunctionDeclaration : AnyDeclaration
    {
        public FunctionDeclaration(Prefix prefix, Name name, GenericParameterClause generics, ParameterClause parameters, FunctionResult? returns, RequirementClause requirements, CodeBlock? block)
        {
            Prefix = prefix;
            Name = name;
            GenericParameters = generics;
            Parameters = parameters;
            Returns = returns;
            Requirements = requirements;
            Block = block;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public GenericParameterClause GenericParameters { get; }
        public ParameterClause Parameters { get; }
        public FunctionResult? Returns { get; }
        public RequirementClause Requirements { get; }
        public CodeBlock? Block { get; }

        public static FunctionDeclaration Parse(Parser parser, Prefix prefix)
        {
            //TODO: is incomplete
            parser.Consume(ToKind.KwFunc);
            var name = Name.Parse(parser, withOperators: true);
            var genericParameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var parameters = ParameterClause.Parse(parser);
            var throws = parser.Match(ToKind.KwThrows);
            var rethrows = parser.Match(ToKind.KwRethrows);
            var returns = parser.Try(ToKind.Arrow, FunctionResult.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
            var block = parser.Try(ToKind.LBrace, CodeBlock.Parse);

            return new FunctionDeclaration(prefix, name, genericParameters, parameters, returns, requirements, block);
        }

        public void Write(IWriter writer)
        {
            var returns = Returns == null ? string.Empty : $" -> {Returns}";
            writer.WriteLine($"func {Name}{GenericParameters}{Parameters}{returns}");
            Block?.Write(writer);
        }
    }
}
