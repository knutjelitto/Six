using SixComp.Support;

namespace SixComp.ParseTree
{
    public class InitializerDeclaration : AnyDeclaration
    {
        public InitializerDeclaration(GenericParameterClause genericParameters, ParameterClause parameters, CodeBlock block)
        {
            GenericParameters = genericParameters;
            Parameters = parameters;
            Block = block;
        }

        public GenericParameterClause GenericParameters { get; }
        public ParameterClause Parameters { get; }
        public CodeBlock Block { get; }

        public static InitializerDeclaration Parse(Parser parser)
        {
            //TODO: incomplete
            parser.Consume(ToKind.KwInit);
            var failable = parser.Match(ToKind.Quest);
            var forced = parser.Match(ToKind.Quest);
            var genericParameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var parameters = ParameterClause.Parse(parser);
            var throws = parser.Match(ToKind.KwThrows);
            var rethrows = parser.Match(ToKind.KwRethrows);
            var requirement = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
            var block = CodeBlock.Parse(parser);

            return new InitializerDeclaration(genericParameters, parameters, block);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"init{GenericParameters}{Parameters}");
            Block.Write(writer);
        }
    }
}
