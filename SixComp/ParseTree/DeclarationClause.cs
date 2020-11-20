using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class DeclarationClause : IWritable
    {
        public DeclarationClause(DeclarationList declarations)
        {
            Declarations = declarations;
        }

        public DeclarationList Declarations { get; }

        public static DeclarationClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LBrace);

            var declarations = DeclarationList.Parse(parser);

            parser.Consume(ToKind.RBrace);

            return new DeclarationClause(declarations);
        }

        public void Write(IWriter writer)
        {
            using (writer.Block())
            {
                Declarations.Write(writer);
            }
        }
    }
}
