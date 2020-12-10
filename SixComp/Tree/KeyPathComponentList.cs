using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathComponentList : ItemList<KeyPathComponent>
        {
            public KeyPathComponentList(List<KeyPathComponent> components) : base(components) { }
            public KeyPathComponentList() { }

            public static KeyPathComponentList Parse(Parser parser)
            {
                var components = new List<KeyPathComponent>();

                do
                {
                    var component = KeyPathComponent.Parse(parser);
                    components.Add(component);
                }
                while (parser.Match(ToKind.Comma));

                return new KeyPathComponentList(components);
            }

            public override string ToString()
            {
                return string.Join(".", this);
            }
        }
    }
}