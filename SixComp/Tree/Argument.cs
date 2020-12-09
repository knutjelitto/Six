﻿using System;

namespace SixComp
{
    public partial class Tree
    {
        public class Argument
        {
            public Argument(ArgumentName? label, AnyExpression value)
            {
                Label = label;
                Value = value;
            }

            public ArgumentName? Label { get; }
            public AnyExpression Value { get; }

            public static Argument Parse(Parser parser)
            {
                var name = ArgumentName.TryParse(parser);

                var expression = AnyExpression.TryParse(parser);
                if (expression == null)
                {
                    if (parser.CurrentToken.IsOperator)
                    {
                        expression = OperatorExpression.Parse(parser);
                    }
                    else
                    {
                        parser.Consume(ToKind.Operator);

                        throw new InvalidOperationException();
                    }
                }

                return new Argument(name, expression);
            }

            public override string ToString()
            {
                var label = Label == null ? string.Empty : $"{Label} ";

                return $"{label}{Value}";
            }
        }
    }
}