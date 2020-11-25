using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class SubscriptDeclaration : AnyDeclaration
    {
        public SubscriptDeclaration(Prefix prefx, GenericParameterClause generics, ParameterClause parameters, FunctionResult result, RequirementClause requirements, GetBlock? getter, SetBlock? setter, CodeBlock? modify, CodeBlock? read)
        {
            Prefx = prefx;
            Generics = generics;
            Parameters = parameters;
            Result = result;
            Requirements = requirements;
            Getter = getter;
            Setter = setter;
            Modify = modify;
            Read = read;
        }

        public Prefix Prefx { get; }
        public GenericParameterClause Generics { get; }
        public ParameterClause Parameters { get; }
        public FunctionResult Result { get; }
        public RequirementClause Requirements { get; }
        public GetBlock? Getter { get; }
        public SetBlock? Setter { get; }
        public CodeBlock? Modify { get; }
        public CodeBlock? Read { get; }

        public static SubscriptDeclaration Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwSubscript);
            var generics = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var parameters = ParameterClause.Parse(parser);
            var result = FunctionResult.Parse(parser);
            var requirements = parser.TryList(ToKind.KwWhere, RequirementClause.Parse);

            var braceOffset = parser.Offset;
            parser.Consume(ToKind.LBrace);

            var getter = (GetBlock?)null;
            var setter = (SetBlock?)null;
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

            if (getter == null && setter == null && modify == null && read == null)
            {
                throw new InvalidOperationException($"{typeof(SubscriptDeclaration)}");
            }

            if (setter != null && getter == null)
            {
                throw new ParserException(setter.Keyword, $"subscript setter must be accompanied by a getter");
            }

            return new SubscriptDeclaration(prefix, generics, parameters, result, requirements, getter, setter, modify, read);
        }
    }
}
