using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
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
                var prefix = Prefix.PreParse(parser);

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

                var braceOffset = parser.Offset;
                parser.Consume(ToKind.LBrace);

                var getter = (GetBlock?)null;
                var setter = (SetBlock?)null;
                var didSetter = (DidSetBlock?)null;
                var willSetter = (WillSetBlock?)null;
                var specials = new Dictionary<string, (int index, CodeBlock block)>();
                var specialsIndex = 0;
                var needBrace = true;
                var mayDefault = true;
                var done = false;

                while (!done)
                {
                    var blockPrefix = Prefix.Parse(parser);

                    switch (parser.Current)
                    {
                        case ToKind.KwGet when getter == null:
                            getter = GetBlock.Parse(parser, blockPrefix);
                            mayDefault = false;
                            break;
                        case ToKind.KwSet when setter == null:
                            setter = SetBlock.Parse(parser, blockPrefix);
                            mayDefault = false;
                            break;
                        case ToKind.KwWillSet:
                            willSetter = WillSetBlock.Parse(parser, blockPrefix);
                            mayDefault = false;
                            break;
                        case ToKind.KwDidSet:
                            didSetter = DidSetBlock.Parse(parser, blockPrefix);
                            mayDefault = false;
                            break;
                        case ToKind.Name when Specials.Contains(parser.CurrentToken.Text):
                            {
                                var key = parser.Consume(ToKind.Name).Text;
                                var index = specialsIndex;
                                specialsIndex += 1;
                                var block = CodeBlock.Parse(parser);
                                specials.Add(key, (index, block));
                                mayDefault = false;
                            }
                            break;
                        default:
                            if (mayDefault)
                            {
                                parser.Offset = braceOffset;  // fallback to simple getter block
                                var token = parser.CurrentToken;
                                var block = CodeBlock.Parse(parser);
                                getter = new GetBlock(Prefix.Empty, token, block);
                                mayDefault = false;
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

                if (getter != null)
                {
                    if (typeAnnotation == null)
                    {
                        throw new ParserException(getter.Keyword, $"computed property must be annotated by a type");
                    }
                    return new GetSetVarDeclaration(prefix, name, typeAnnotation, getter, setter, specials);
                }
                if (setter != null)
                {
                    throw new ParserException(setter.Keyword, $"property setter must be accompanied by a getter");
                }
                return new WillDidVarDeclaration(prefix, name, typeAnnotation, initializer, willSetter, didSetter);
            }
        }
    }
}
