using System;

namespace SixComp.ParseTree
{
    public class Parameter
    {
        public Parameter(Name? label, Name name, IType type)
        {
            Label = label;
            Name = name;
            Type = type;
        }

        public Name? Label { get; }
        public Name Name { get; }
        public Type Type { get; }

        public static Parameter Parse(Parser parser)
        {
            Name? label = null;

            if (parser.Next.Kind == ToKind.Name)
            {
                label = Name.Parse(parser);
            }

            var name = Name.Parse(parser);

            parser.Consume(ToKind.Colon);
        }
    }
}
