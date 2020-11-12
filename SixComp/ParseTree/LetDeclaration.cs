using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class LetDeclaration : Declaration
    {
        public LetDeclaration(Name name, IType? type, Expression? initializer)
        {
            Name = name;
            Type = type;
            Initializer = initializer;
        }

        public Name Name { get; }
        public IType? Type { get; }
        public Expression? Initializer { get; }

        public override void Write(IWriter writer)
        {
            writer.Write($"let {Name}");
            if (Type is IType t)
            {
                writer.Write($": {t}");
            }
            if (Initializer is Expression e)
            {
                writer.Write($"= {e}");
            }
            writer.WriteLine();
        }
    }
}
