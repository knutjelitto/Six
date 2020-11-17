using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class AttributeList : ItemList<Attribute>
    {
        public AttributeList(List<Attribute> attributes) : base(attributes) { }
        public AttributeList() { }

        public static AttributeList TryParse(Parser parser)
        {
            if (parser.Current == ToKind.At)
            {
                var attributes = new List<Attribute>();

                do
                {
                    var attribute = Attribute.Parse(parser);
                    attributes.Add(attribute);
                }
                while (parser.Current == ToKind.At);

                return new AttributeList(attributes);
            }

            return new AttributeList();
        }
    }
}
