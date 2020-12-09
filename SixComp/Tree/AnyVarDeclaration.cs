using SixComp.Common;
using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp
{
    public partial class Tree
    {
        public interface AnyVarDeclaration : AnyDeclaration
        {
            public static HashSet<string> Specials = new HashSet<string>
        {
            "_modify",
            "_read",
            "unsafeAddress",
            "unsafeMutableAddress",
        };

            public static bool CheckWillSetDitSet(Parser parser)
            {
                if (parser.Current == ToKind.KwWillSet || parser.Current == ToKind.KwDidSet)
                {
                    return true;
                }

                using (parser.InBacktrack())
                {
                    // skip over attributes
                    var prefix = Prefix.Parse(parser);

                    if (prefix.Last == ToKind.KwWillSet || prefix.Last == ToKind.KwDidSet)
                    {
                        return true;
                    }
                }

                return false;
            }

            public static AnyVarDeclaration Parse(Parser parser, Prefix prefix)
            {
                // already parsed //parser.Consume(ToKind.KwVar);

                var patternOffset = parser.Offset;

                var pattInit = PatternInitializer.Parse(parser);
                var pattInits = new List<PatternInitializer>
            {
                pattInit
            };

                while (parser.Match(ToKind.Comma))
                {
                    pattInit = PatternInitializer.Parse(parser);
                    pattInits.Add(pattInit);
                }
                if (pattInits.Count >= 2 || parser.Current != ToKind.LBrace)
                {
                    return PatternVarDeclaration.From(prefix, PatternInitializerList.From(pattInits));
                }
                else
                {
                    if (!(pattInit.Pattern is IdentifierPattern ip))
                    {
                        throw new ParserException(parser.At(patternOffset), $"no pattern allowed for set/get/willSet/ditSet");
                    }

                    // destruct pattern initializer

                    var name = ip.Name;
                    var typeAnnotation = pattInit.Type;
                    var initializer = pattInit.Initializer;
                    if (initializer != null)
                    {
                        throw new NotImplementedException();
                    }

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
                                blocks.Add(PropertyBlock.Parse(parser, blockPrefix, BlockKind.Get));
                                break;
                            case ToKind.KwSet when !blocks.Have(parser.Current):
                                blocks.Add(PropertyBlock.Parse(parser, blockPrefix, BlockKind.Set));
                                break;
                            case ToKind.KwWillSet when !blocks.Have(parser.Current):
                                blocks.Add(PropertyBlock.Parse(parser, blockPrefix, BlockKind.WillSet));
                                break;
                            case ToKind.KwDidSet when !blocks.Have(parser.Current):
                                blocks.Add(PropertyBlock.Parse(parser, blockPrefix, BlockKind.DidSet));
                                break;
                            case ToKind.Name when Specials.Contains(parser.CurrentToken.Text):
                                blocks.Add(PropertyBlock.Parse(parser, blockPrefix, BlockKind.Special));
                                break;
                            default:
                                if (blocks.Count == 0)
                                {
                                    parser.Offset = braceOffset;  // fallback to simple getter block
                                    blocks.Add(PropertyBlock.Parse(parser, blockPrefix, BlockKind.GetDefault));
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

                    return new VarDeclaration(prefix, name, typeAnnotation, blocks);
                }
            }
        }
    }
}