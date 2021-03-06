﻿using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public class FunctionTypeArgument
        {
            public FunctionTypeArgument(BaseName? @extern, IType type)
            {
                Extern = @extern;
                Type = type;
            }

            public BaseName? Extern { get; }
            public IType Type { get; }

            public static FunctionTypeArgument Parse(Parser parser)
            {
                BaseName? label = null;
                IType type;

                if (parser.Next == ToKind.Colon)
                {
                    label = BaseName.Parse(parser);
                    type = TypeAnnotation.Parse(parser);
                }
                else if (parser.NextNext == ToKind.Colon)
                {
                    label = BaseName.Parse(parser);
                    Debug.Assert(label.ToString() == "_");
                    label = BaseName.Parse(parser);
                    type = TypeAnnotation.Parse(parser);
                }
                else
                {
                    type = PrefixedType.Parse(parser);
                }

                return new FunctionTypeArgument(label, type);
            }

            public override string ToString()
            {
                var label = Extern == null ? string.Empty : $"{Extern} ";
                return $"{label}{Type}";
            }
        }
    }
}