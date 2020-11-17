using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class PropertyGetBlock : AnyPropertyBlock
    {
        public PropertyGetBlock(Prefix prefix, CodeBlock? block)
            : base(prefix, null, block)
        {
        }

        public static PropertyGetBlock Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwGet);

            var block = parser.Current == ToKind.LBrace
                ? CodeBlock.Parse(parser)
                : null;

            return new PropertyGetBlock(prefix, block);
        }
    }
}
