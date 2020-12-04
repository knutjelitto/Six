using SixComp.Support;

namespace SixComp.Tree
{
    public class VarDeclaration : AnyVarDeclaration
    {
        public VarDeclaration(Prefix prefix, BaseName name, TypeAnnotation? type, PropertyBlocks blocks)
        {
            Prefix = prefix;
            Name = name;
            Type = type;
            Blocks = blocks;
        }

        public Prefix Prefix { get; }
        public BaseName Name { get; }
        public TypeAnnotation? Type { get; }
        public PropertyBlocks Blocks { get; }

        public void Write(IWriter writer)
        {
            Prefix.Write(writer);
            writer.WriteLine($"{Name}{Type}{Blocks}");
        }

        public override string ToString()
        {
            return $"{Prefix}{Name}{Type}{Blocks}";
        }
    }
}
