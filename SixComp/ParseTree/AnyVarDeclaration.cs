using System;
using System.Diagnostics;
using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyVarDeclaration : AnyDeclaration
    {
        public static AnyVarDeclaration Parse(Parser parser, Prefix prefix)
        {
            //TODO: incomplete

            parser.Consume(ToKind.KwVar);

            if (parser.Current == ToKind.LParent)
            {
                return PatternVarDeclaration.Parse(parser, prefix);
            }

            var offset = parser.Offset; // for backtrace

            // first try complicated property
            {
                var name = Name.Parse(parser);
                var typeAnnotation = parser.Try(ToKind.Colon, TypeAnnotation.Parse);
                var initializer = parser.Try(ToKind.Equal, Initializer.Parse);

                if (parser.Current == ToKind.LBrace)
                {
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
                                    parser.Offset = braceOffset;  // fallback to simple code block
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


                parser.Offset = offset;

                return PatternVarDeclaration.Parse(parser, prefix);
            }

        }
    }
}
