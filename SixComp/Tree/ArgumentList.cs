using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ArgumentList : ItemList<ArgumentList.Argument>
        {
            public ArgumentList(List<Argument> arguments) : base(arguments) { }
            public ArgumentList() { }

            public static ArgumentList Parse(Parser parser)
            {
                var arguments = new List<Argument>();

                if (parser.Current != ToKind.RParent)
                {
                    do
                    {
                        var argument = Argument.Parse(parser);
                        arguments.Add(argument);
                    }
                    while (parser.Match(ToKind.Comma));
                }

                return new ArgumentList(arguments);
            }

            public override string ToString()
            {
                return string.Join(", ", this.Select(a => a.StripParents()));
            }

            public class Argument
            {
                public Argument(ArgumentName? label, IExpression value)
                {
                    Label = label;
                    Value = value;
                }

                public ArgumentName? Label { get; }
                public IExpression Value { get; }

                public static Argument Parse(Parser parser)
                {
                    var name = ArgumentName.TryParse(parser);

                    var expression = IExpression.TryParse(parser);
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
}