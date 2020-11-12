using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Unit : IWriteable
    {
        public Unit(DeclarationList declarations)
        {
            Declarations = declarations;
        }

        public DeclarationList Declarations { get; }

        public void Write(IWriter writer)
        {
            Declarations.Write(writer);
        }
    }
}
