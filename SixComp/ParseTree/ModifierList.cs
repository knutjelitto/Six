using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ModifierList : ItemList<Modifier>
    {
        private ModifierList(List<Modifier> modifiers) : base(modifiers) { }
        public ModifierList() { }

        public static ModifierList TryParse(Parser parser)
        {
            if (Modifier.Firsts.Contains(parser.Current))
            {
                var modifiers = new List<Modifier>();

                do
                {
                    var modifier = Modifier.Parse(parser);
                    modifiers.Add(modifier);
                }
                while (Modifier.Firsts.Contains(parser.Current));

                return new ModifierList(modifiers);
            }

            return new ModifierList();
        }

        public override string ToString()
        {
            if (Count > 0)
            {
                return string.Join(" ", this) + " ";
            }
            return string.Empty;
        }
    }
}
