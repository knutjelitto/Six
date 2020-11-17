using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Unit : IWritable
    {

        public Unit(DeclarationList declarations)
        {
            Declarations = declarations;
        }

        public DeclarationList Declarations { get; }

        public void Write(IWriter writer)
        {
            bool more = false;

            foreach (var declaration in Declarations)
            {
                if (more)
                {
                    writer.WriteLine();
                }
                declaration.Write(writer);
                more = true;
            }
        }

        public static Unit Parse(Parser parser)
        {
            var declarations = DeclarationList.Parse(parser);

            return new Unit(declarations);
        }
    }
}
