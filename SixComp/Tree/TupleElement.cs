﻿using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TupleElement : SyntaxNode
        {
            public TupleElement(BaseName? name, IExpression value)
            {
                Name = name;
                Value = value;
            }

            public BaseName? Name { get; }
            public IExpression Value { get; }

            public static TupleElement Parse(Parser parser)
            {
                BaseName? name = null;
                if (parser.Next == ToKind.Colon)
                {
                    name = BaseName.Parse(parser);
                    parser.Consume(ToKind.Colon);
                }
                var value = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(TupleElement)}");

                return new TupleElement(name, value);
            }

            public override string ToString()
            {
                var name = Name == null ? string.Empty : $"{Name}: ";
                return $"{name}{Value}";
            }
        }
    }
}