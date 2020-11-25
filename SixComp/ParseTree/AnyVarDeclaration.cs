using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public interface AnyVarDeclaration : AnyDeclaration
    {
        public static bool CheckWillDit(Parser parser)
        {
            if (parser.Current == ToKind.KwWillSet || parser.Current == ToKind.KwDidSet)
            {
                return true;
            }

            using (parser.InBacktrack())
            {
                // skip over attributes
                Prefix.Parse(parser);

                if (parser.Current == ToKind.KwWillSet || parser.Current == ToKind.KwDidSet)
                {
                    return true;
                }
            }

            return false;
        }

        public static AnyVarDeclaration Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwVar);

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
                var typeAnnotation = ip.Type;
                var initializer = pattInit.Initializer;

                var braceOffset = parser.Offset;
                parser.Consume(ToKind.LBrace);

                var getter = (GetBlock?)null;
                var setter = (SetBlock?)null;
                var didSetter = (DidSetBlock?)null;
                var willSetter = (WillSetBlock?)null;
                var modify = (CodeBlock?)null;
                var read = (CodeBlock?)null;
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
                        case ToKind.Kw_Modify when modify == null:
                            parser.Consume(ToKind.Kw_Modify);
                            modify = CodeBlock.Parse(parser);
                            mayDefault = false;
                            break;
                        case ToKind.Kw_Read when read == null:
                            parser.Consume(ToKind.Kw_Read);
                            read = CodeBlock.Parse(parser);
                            mayDefault = false;
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
                            else
                            {
                                done = true;
                            }
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
                    return new GetSetVarDeclaration(prefix, name, typeAnnotation, getter, setter, modify, read);
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
