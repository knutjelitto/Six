using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class SubscriptDeclaration : AnyDeclaration
    {
        public SubscriptDeclaration(Prefix prefx, GenericParameterClause generics, ParameterClause parameters, FunctionResult result, RequirementClause requirements, GetBlock? getter, SetBlock? setter, Dictionary<string, (int index, CodeBlock block)> specials)
        {
            Prefix = prefx;
            Generics = generics;
            Parameters = parameters;
            Result = result;
            Requirements = requirements;
            Getter = getter;
            Setter = setter;
            Specials = specials;
        }

        public Prefix Prefix { get; }
        public GenericParameterClause Generics { get; }
        public ParameterClause Parameters { get; }
        public FunctionResult Result { get; }
        public RequirementClause Requirements { get; }
        public GetBlock? Getter { get; }
        public SetBlock? Setter { get; }
        public Dictionary<string, (int index, CodeBlock block)> Specials { get; }

        public static SubscriptDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwSubscript);

            var generics = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var parameters = ParameterClause.Parse(parser);
            var result = FunctionResult.Parse(parser);
            var requirements = parser.TryList(ToKind.KwWhere, RequirementClause.Parse);

            var braceOffset = parser.Offset;
            parser.Consume(ToKind.LBrace);

            var getter = (GetBlock?)null;
            var setter = (SetBlock?)null;
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
                    case ToKind.Name when AnyVarDeclaration.Specials.Contains(parser.CurrentToken.Text):
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

            if (getter == null && setter == null && specials.Count == 0)
            {
                throw new InvalidOperationException($"{typeof(SubscriptDeclaration)}");
            }

            if (setter != null && getter == null)
            {
                throw new ParserException(setter.Keyword, $"subscript setter must be accompanied by a getter");
            }

            return new SubscriptDeclaration(prefix, generics, parameters, result, requirements, getter, setter, specials);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Prefix}{Generics}{Parameters}{Result}");
            writer.WriteLine("//TODO");
        }
    }
}
