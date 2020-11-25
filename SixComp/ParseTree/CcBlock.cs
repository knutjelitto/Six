namespace SixComp.ParseTree
{
    public class CcBlock : AnyDeclaration
    {
        public CcBlock()
        {
        }

        public static CcBlock Parse(Parser parser)
        {
            parser.Consume(ToKind.CdIf);

            while (parser.Current != ToKind.CdEndif)
            {
                if (parser.Current == ToKind.CdIf)
                {
                    CcBlock.Parse(parser);
                }
                else
                {
                    parser.ConsumeAny();
                }
            }

            parser.Consume(ToKind.CdEndif);

            return new CcBlock();
        }

        public static void Ignore(Parser parser)
        {
            //TODO: completely ignored `#if´
            if (parser.Current == ToKind.CdIf)
            {
                Parse(parser);
            }
        }
    }
}
