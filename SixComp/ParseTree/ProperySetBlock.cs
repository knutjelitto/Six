using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class PropertySetBlock : AnyPropertyBlock
    {
        public PropertySetBlock(Prefix prefix, Name? setterName, CodeBlock? block)
            : base(prefix, setterName, block)
        {
        }

        public static PropertySetBlock Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwSet);

            var name = (Name?)null;
            if (parser.Match(ToKind.LParent))
            {
                name = Name.Parse(parser);
                parser.Consume(ToKind.RParent);
            }

            var block = parser.Current == ToKind.LBrace
                ? CodeBlock.Parse(parser)
                : null;

            return new PropertySetBlock(prefix, name, block);
        }
    }
}
