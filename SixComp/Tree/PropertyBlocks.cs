using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Tree
{
    public sealed class PropertyBlocks : Dictionary<BaseName, (int index, PropertyBlock block)>
    {
        private int index = 0;
        private TokenSet have = new TokenSet();

        public void Add(PropertyBlock block)
        {
            have.Add(block.Keyword.Kind);
            Add(block.BlockName, (index, block));
            index += 1;
        }

        public bool Have(ToKind kind)
        {
            return have.Contains(kind);
        }

        public bool Have(Token token)
        {
            return have.Contains(token.Kind);
        }
    }
}
