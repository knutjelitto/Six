using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class SubscriptDeclaration : AnyDeclaration
    {
        public SubscriptDeclaration(Prefix prefx, GenericParameterClause generics, ParameterClause parameters, FunctionResult result, RequirementClause requirements, GetBlock getter, SetBlock? setter, CodeBlock? modify)
        {
            Prefx = prefx;
            Generics = generics;
            Parameters = parameters;
            Result = result;
            Requirements = requirements;
            Getter = getter;
            Setter = setter;
            Modify = modify;
        }

        public Prefix Prefx { get; }
        public GenericParameterClause Generics { get; }
        public ParameterClause Parameters { get; }
        public FunctionResult Result { get; }
        public RequirementClause Requirements { get; }
        public GetBlock Getter { get; }
        public SetBlock? Setter { get; }
        public CodeBlock? Modify { get; }

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
                    case ToKind.Kw_Modify:
                        parser.Consume(ToKind.Kw_Modify);
                        modify = CodeBlock.Parse(parser);
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

            if (count != 0)
            {
                parser.Consume(ToKind.RBrace);
            }

            if (getter == null)
            {
                throw new InvalidOperationException($"{typeof(SubscriptDeclaration)}");
            }

            return new SubscriptDeclaration(prefix, generics, parameters, result, requirements, getter, setter, modify);
        }
    }
}
