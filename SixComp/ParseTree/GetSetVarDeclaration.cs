using SixComp.Support;

namespace SixComp.ParseTree
{
    public class GetSetVarDeclaration : AnyVarDeclaration
    {
        public GetSetVarDeclaration(Prefix prefix, Name name, TypeAnnotation type, GetBlock getter, SetBlock? setter, CodeBlock? modify, CodeBlock? read)
        {
            Prefix = prefix;
            Name = name;
            Type = type;
            Getter = getter;
            Setter = setter;
            Modify = modify;
            Read = read;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public TypeAnnotation Type { get; }
        public GetBlock Getter { get; }
        public SetBlock? Setter { get; }
        public CodeBlock? Modify { get; }
        public CodeBlock? Read { get; }

        public override string ToString()
        {
            return $"{Prefix}{Name}{Type}{Getter}{Setter.Str()}";
        }
    }
}
