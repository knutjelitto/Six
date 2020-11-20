namespace SixComp.ParseTree
{
    public class GetSetVarDeclaration : AnyVarDeclaration
    {
        public GetSetVarDeclaration(Prefix prefix, Name name, TypeAnnotation type, GetBlock getter, SetBlock? setter)
        {
            Prefix = prefix;
            Name = name;
            Type = type;
            Getter = getter;
            Setter = setter;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public TypeAnnotation Type { get; }
        public GetBlock Getter { get; }
        public SetBlock? Setter { get; }
    }
}
