using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.ParseTree
{
    public class WillDidVarDeclaration : AnyVarDeclaration
    {
        public WillDidVarDeclaration(Prefix prefix, Name name, TypeAnnotation? type, Initializer? initializer, WillSetBlock? willSet, DidSetBlock? didSet, Dictionary<string, (int index, CodeBlock block)> specials)
        {
            Prefix = prefix;
            Name = name;
            Type = type;
            Initializer = initializer;
            WillSet = willSet;
            DidSet = didSet;
            Specials = specials;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public TypeAnnotation? Type { get; }
        public Initializer? Initializer { get; }
        public WillSetBlock? WillSet { get; }
        public DidSetBlock? DidSet { get; }
        public Dictionary<string, (int index, CodeBlock block)> Specials { get; }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Prefix}{Name}{Type}{Initializer}");
            using (writer.Block())
            {
                WillSet?.Write(writer);
                DidSet?.Write(writer);
                IEnumerable<(string name, CodeBlock block)>? specials = Specials
                    .OrderBy(s => s.Value.index)
                    .Select(s => (s.Key, s.Value.block));
                foreach (var special in specials)
                {
                    writer.Write($"{special.name}");
                    var block = $"{special.block}";
                    if (block.Length <= 100)
                    {
                        writer.WriteLine($"{block}");
                    }
                    else
                    {
                        writer.WriteLine();
                        special.block.Write(writer);
                    }
                }
            }
        }
    }
}
