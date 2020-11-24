﻿using SixComp.Support;

namespace SixComp.ParseTree
{
    public class DeinitializerDeclaration : AnyDeclaration
    {
        public DeinitializerDeclaration(Prefix prefix, CodeBlock block)
        {
            Prefix = prefix;
            Block = block;
        }

        public Prefix Prefix { get; }
        public CodeBlock Block { get; }

        public static DeinitializerDeclaration Parse(Parser parser, Prefix prefix)
        {
            //TODO: is incomplete
            parser.Consume(ToKind.KwDeinit);
            var block = CodeBlock.Parse(parser);

            return new DeinitializerDeclaration(prefix, block);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"deinit");
            Block.Write(writer);
        }
    }
}
