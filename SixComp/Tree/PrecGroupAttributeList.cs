using System.Collections.Generic;

namespace SixComp.Tree
{
    public class PrecGroupAttributeList : ItemList<PrecGroupAttribute>
    {
        public PrecGroupAttributeList(List<PrecGroupAttribute> attributes) : base(attributes) { }
        public PrecGroupAttributeList() { }

        public static PrecGroupAttributeList Parse(Parser parser)
        {
            var attributes = new List<PrecGroupAttribute>();
            PrecGroupAttribute? attribute;
            while ((attribute = PrecGroupAttribute.TryParse(parser)) != null)
            {
                attributes.Add(attribute);
            }

            return new PrecGroupAttributeList(attributes);
        }
    }
}
