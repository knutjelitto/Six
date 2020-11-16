using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class InitializerDeclaration : AnyDeclaration
    {
        public InitializerDeclaration(GenericParameterList genericParameters, ParameterList parameters, CodeBlock block)
        {
            GenericParameters = genericParameters;
            Parameters = parameters;
            Block = block;
        }

        public GenericParameterList GenericParameters { get; }
        public ParameterList Parameters { get; }
        public CodeBlock Block { get; }

        public static InitializerDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwInit);
            var genericParameters = parser.TryList(ToKind.Less, GenericParameterList.Parse);
            var parameters = ParameterList.Parse(parser);
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
