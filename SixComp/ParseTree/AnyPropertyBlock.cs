namespace SixComp.ParseTree
{
    public abstract class AnyPropertyBlock
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
    }
}
