using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class TupleElement : SyntaxNode
    {
        public TupleElement(Name? name, AnyExpression value)
        {
            Name = name;
            Value = value;
        }

        public Name? Name { get; }
        public AnyExpression Value { get; }

        public static TupleElement Parse(Parser parser)
        {
            Name? name = null;
            if (parser.Next == ToKind.Colon)
            {
                name = Name.Parse(parser);
                parser.Consume(ToKind.Colon);
            }
            var value = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException();

            return new TupleElement(name, value);
        }

        public override string ToString()
        {
            var name = Name == null ? string.Empty : $"{Name}: ";
            return $"{name}{Value}";
        }
    }
}
