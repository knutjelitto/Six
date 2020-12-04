using SixComp.Common;
using SixComp.Support;
using System.Diagnostics;

namespace SixComp.Tree
{
    public sealed class PropertyBlock : IWritable
    {
        private PropertyBlock(Prefix prefix, BlockKind kind, Token keyword, SetterName? setterName, CodeBlock? block)
        {
            Prefix = prefix;
            Kind = kind;
            Keyword = keyword;
            BlockName = BaseName.From(Keyword);
            SetterName = setterName;
            Block = block;
        }

        public Prefix Prefix { get; }
        public BlockKind Kind { get; }
        public Token Keyword { get; }
        public BaseName BlockName { get; }
        public SetterName? SetterName { get; }
        public CodeBlock? Block { get; }

        public static PropertyBlock Parse(Parser parser, Prefix prefix, BlockKind kind)
        {
            var keyword = parser.CurrentToken;

            CodeBlock? block = null;
            SetterName? setterName = null;

            if (kind == BlockKind.GetDefault)
            {
                Debug.Assert(CodeBlock.Firsts.Contains(parser.Current));
                block = CodeBlock.Parse(parser);
            }
            else
            {
                keyword = parser.ConsumeAny();
                setterName = parser.Try(SetterName.Firsts, SetterName.Parse);
                block = parser.Current == ToKind.LBrace
                    ? CodeBlock.Parse(parser)
                    : null;
            }

            return new PropertyBlock(prefix, kind, keyword, setterName, block);
        }

        public override string ToString()
        {
            if (Keyword.Kind == ToKind.LBrace)
            {
                return $" {Block}";
            }
            return $" {Keyword} {Block}";
        }

        public void Write(IWriter writer)
        {
            if (Keyword.Kind == ToKind.LBrace)
            {
                Block?.Write(writer);
            }
        }
    }
}
