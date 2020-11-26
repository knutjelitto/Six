using SixComp.Support;

namespace SixComp.ParseTree
{
    public abstract class AnyPropertyBlock : IWritable
    {
        protected AnyPropertyBlock(Prefix prefix, Token keyword, SetterName? setterName, CodeBlock? block)
        {
            Prefix = prefix;
            Keyword = keyword;
            SetterName = setterName;
            Block = block;
        }

        public Prefix Prefix { get; }
        public Token Keyword { get; }
        public SetterName? SetterName { get; }
        public CodeBlock? Block { get; }

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
