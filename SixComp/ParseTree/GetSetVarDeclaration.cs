using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class GetSetVarDeclaration : AnyVarDeclaration
    {
        public GetSetVarDeclaration(Prefix prefix, Name name, TypeAnnotation type, GetBlock getter, SetBlock? setter, Dictionary<string, (int index, CodeBlock block)> specials)
        {
            Prefix = prefix;
            Name = name;
            Type = type;
            Getter = getter;
            Setter = setter;
            Specials = specials;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public TypeAnnotation Type { get; }
        public GetBlock Getter { get; }
        public SetBlock? Setter { get; }
        public Dictionary<string, (int index, CodeBlock block)> Specials { get; }

        public override string ToString()
        {
            return $"{Prefix}{Name}{Type}{Getter}{Setter.Str()}";
        }
    }
}
