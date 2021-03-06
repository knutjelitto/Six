﻿using Six.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class DeclarationClause : IWritable
        {
            public DeclarationClause(DeclarationList declarations)
            {
                Declarations = declarations;
            }

            public DeclarationList Declarations { get; }

            public static DeclarationClause Parse(Parser parser, IDeclaration.Context context)
            {
                parser.Consume(ToKind.LBrace);

                var declarations = DeclarationList.Parse(parser, context);

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

            public override string ToString()
            {
                return $" {{ {Declarations} }}";
            }
        }
    }
}