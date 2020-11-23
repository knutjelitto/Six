﻿using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ModifierList : ItemList<DeclarationModifier>
    {
        private ModifierList(List<DeclarationModifier> modifiers) : base(modifiers) { }
        public ModifierList() { }

        public static ModifierList TryParse(Parser parser, bool exclude)
        {
            var modifiers = new List<DeclarationModifier>();

            DeclarationModifier? modifier;
            while ((modifier = DeclarationModifier.TryParse(parser, exclude)) != null)
            {
                modifiers.Add(modifier);
            }

            return new ModifierList(modifiers);
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
