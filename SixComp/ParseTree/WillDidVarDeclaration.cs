namespace SixComp.ParseTree
{
    public class WillDidVarDeclaration : AnyVarDeclaration
    {
        public WillDidVarDeclaration(Prefix prefix, Name name, TypeAnnotation? type, Initializer? initializer, WillSetBlock? willSet, DidSetBlock? didSet)
        {
            Prefix = prefix;
            Name = name;
            Type = type;
            Initializer = initializer;
            WillSet = willSet;
            DidSet = didSet;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public TypeAnnotation? Type { get; }
        public Initializer? Initializer { get; }
        public WillSetBlock? WillSet { get; }
        public DidSetBlock? DidSet { get; }
    }
}
