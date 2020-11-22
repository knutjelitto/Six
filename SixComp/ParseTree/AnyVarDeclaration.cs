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
                var count = 0;
                var done = false;

                while (!done)
                {
                    var blockPrefix = Prefix.Parse(parser);

                    switch (parser.Current)
                    {
                        case ToKind.KwGet:
                            getter = GetBlock.Parse(parser, blockPrefix);
                            count += 1;
                            break;
                        case ToKind.KwSet:
                            setter = SetBlock.Parse(parser, blockPrefix);
                            count += 1;
                            break;
                        case ToKind.KwWillSet:
                            willSetter = WillSetBlock.Parse(parser, blockPrefix);
                            count += 1;
                            break;
                        case ToKind.KwDidSet:
                            didSetter = DidSetBlock.Parse(parser, blockPrefix);
                            count += 1;
                            break;
                        default:
                            if (count == 0)
                            {
                                parser.Offset = braceOffset;  // fallback to simple getter block
                                var token = parser.CurrentToken;
                                var block = CodeBlock.Parse(parser);
                                getter = new GetBlock(Prefix.Empty, token, block);
                            }
                            done = true;
                            break;
                    }
                }

                if (count != 0 || getter == null)
                {
                    parser.Consume(ToKind.RBrace);
                }

                if (getter != null)
                {
                    if (typeAnnotation == null)
                    {
                        throw new ParserException(getter.Keyword, $"computed property must be annotated by a type");
                    }
                    return new GetSetVarDeclaration(prefix, name, typeAnnotation, getter, setter);
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
