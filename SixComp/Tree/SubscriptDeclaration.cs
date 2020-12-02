using SixComp.Support;

namespace SixComp.Tree
{
    public class SubscriptDeclaration : AnyDeclaration
    {
        public SubscriptDeclaration(Prefix prefx, GenericParameterClause generics, ParameterClause parameters, FunctionResult result, RequirementClause requirements, PropertyBlocks blocks)
        {
            Prefix = prefx;
            Generics = generics;
            Parameters = parameters;
            Result = result;
            Requirements = requirements;
            Blocks = blocks;
        }

        public Prefix Prefix { get; }
        public GenericParameterClause Generics { get; }
        public ParameterClause Parameters { get; }
        public FunctionResult Result { get; }
        public RequirementClause Requirements { get; }
        public PropertyBlocks Blocks { get; }

        public static SubscriptDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwSubscript);

            var generics = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var parameters = ParameterClause.Parse(parser);
            var result = FunctionResult.Parse(parser);
            var requirements = parser.TryList(ToKind.KwWhere, RequirementClause.Parse);

            var braceOffset = parser.Offset;
            parser.Consume(ToKind.LBrace);

            var needBrace = true;
            var done = false;

            var blocks = new PropertyBlocks();

            while (!done)
            {
                var blockPrefix = Prefix.Parse(parser);

                switch (parser.Current)
                {
                    case ToKind.KwGet when !blocks.Have(parser.Current):
                        blocks.Add(PropertyBlock.Parse(parser, prefix, PropertyBlock.BlockKind.Get));
                        break;
                    case ToKind.KwSet when !blocks.Have(parser.Current):
                        blocks.Add(PropertyBlock.Parse(parser, prefix, PropertyBlock.BlockKind.Set));
                        break;
                    case ToKind.Name when AnyVarDeclaration.Specials.Contains(parser.CurrentToken.Text):
                        blocks.Add(PropertyBlock.Parse(parser, prefix, PropertyBlock.BlockKind.Special));
                        break;
                    default:
                        if (blocks.Count == 0)
                        {
                            parser.Offset = braceOffset;  // fallback to simple getter block
                            blocks.Add(PropertyBlock.Parse(parser, prefix, PropertyBlock.BlockKind.GetDefault));
                            needBrace = false;
                        }
                        done = true;
                        break;
                }
            }

            if (needBrace)
            {
                parser.Consume(ToKind.RBrace);
            }

            return new SubscriptDeclaration(prefix, generics, parameters, result, requirements, blocks);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Prefix}{Generics}{Parameters}{Result}");
            writer.WriteLine("//TODO");
        }
    }
}
