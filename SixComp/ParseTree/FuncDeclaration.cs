using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class FuncDeclaration : Declaration
    {
        public FuncDeclaration(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public override void Write(IWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
