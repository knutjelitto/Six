namespace SixComp
{
    public partial class ParseTree
    {
        public class CcBlock : IDeclaration
        {
            public CcBlock()
            {
            }

            private static CcBlock Parse(Parser parser, bool consumeIf)
            {
                if (consumeIf)
                {
                    parser.Consume(ToKind.CdIf);
                }

                while (parser.Current != ToKind.CdEndif)
                {
                    if (parser.Current == ToKind.CdIf)
                    {
                        CcBlock.Parse(parser, true);
                    }
                    else
                    {
                        parser.ConsumeAny();
                    }
                }

                parser.Consume(ToKind.CdEndif);

                return new CcBlock();
            }

            public static void Ignore(Parser parser, bool force)
            {
                //TODO: completely ignored `#if´
                if (force)
                {
                    Parse(parser, false);
                }
                else if (parser.Current == ToKind.CdIf)
                {
                    Parse(parser, true);
                }
            }
        }
    }
}