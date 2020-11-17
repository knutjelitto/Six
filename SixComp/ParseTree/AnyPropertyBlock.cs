using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public abstract class AnyPropertyBlock
    {
        public AnyPropertyBlock(Prefix prefix, Name? setterName, CodeBlock? block)
        {
            Prefix = prefix;
            SetterName = setterName;
            Block = block;
        }

        public Prefix Prefix { get; }
        public Name? SetterName { get; }
        public CodeBlock? Block { get; }
    }
}
