using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class PrecGroupAttributeList : ItemList<PrecGroupAttribute>
    {
        public PrecGroupAttributeList(List<PrecGroupAttribute> attributes) : base(attributes) { }
        public PrecGroupAttributeList() { }

        public static PrecGroupAttributeList Parse(Parser parser)
        {
            var attributes = new List<PrecGroupAttribute>();
            while (PrecGroupAttribute.Firsts.Contains(parser.Current))
            {
                var attribute = PrecGroupAttribute.Parse(parser);
                attributes.Add(attribute);
            }

            return new PrecGroupAttributeList(attributes);
        }
    }
}
