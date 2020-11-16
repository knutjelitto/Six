﻿using SixComp.Support;

namespace SixComp.ParseTree
{
    public class FuncDeclaration : AnyDeclaration
    {
        public FuncDeclaration(Name name, GenericParameterList genericParameters, ParameterList parameters, AnyType? returns, CodeBlock block)
        {
            Name = name;
            GenericParameters = genericParameters;
            Parameters = parameters;
            Returns = returns;
            Block = block;
        }

        public Name Name { get; }
        public GenericParameterList GenericParameters { get; }
        public ParameterList Parameters { get; }
        public AnyType? Returns { get; }
        public CodeBlock Block { get; }

        public static FuncDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwFunc);
            var name = Name.Parse(parser);
            var genericParameters = parser.TryList(ToKind.Less, GenericParameterList.Parse);
            var parameters = ParameterList.Parse(parser);
            var returns = parser.TryMatch(ToKind.MinusGreater, AnyType.Parse);
            var block = CodeBlock.Parse(parser);

            return new FuncDeclaration(name, genericParameters, parameters, returns, block);
        }

        public void Write(IWriter writer)
        {
            var returns = Returns == null ? string.Empty : $" -> {Returns}";
            writer.WriteLine($"func {Name}{GenericParameters}{Parameters}{returns}");
            Block.Write(writer);
        }
    }
}
