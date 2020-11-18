namespace SixComp.ParseTree
{
    public class Prefix
    {
        private Prefix(AttributeList attributes, ModifierList modifiers)
        {
            Attributes = attributes;
            Modifiers = modifiers;
        }

        public AttributeList Attributes { get; }
        public ModifierList Modifiers { get; }

        public static Prefix Parse(Parser parser)
        {
            var attributes = AttributeList.TryParse(parser);
            var modifiers = ModifierList.TryParse(parser);

            return new Prefix(attributes, modifiers);
        }

        public override string ToString()
        {
            return $"{Attributes}{Modifiers}";
        }
    }
}
