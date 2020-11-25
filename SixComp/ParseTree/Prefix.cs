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

        public static Prefix Parse(Parser parser, bool exclude = true)
        {
            var attributes = AttributeList.TryParse(parser);
            var modifiers = ModifierList.TryParse(parser, exclude);

            return new Prefix(attributes, modifiers);
        }

        public static readonly Prefix Empty = new Prefix(new AttributeList(), new ModifierList());

        public bool Missing => Attributes.Missing && Modifiers.Missing;

        public override string ToString()
        {
            return $"{Attributes}{Modifiers}";
        }
    }
}
