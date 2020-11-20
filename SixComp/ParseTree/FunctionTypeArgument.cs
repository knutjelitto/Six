﻿namespace SixComp.ParseTree
{
    public class FunctionTypeArgument
    {
        public FunctionTypeArgument(Name? label, AnyType type)
        {
            Label = label;
            Type = type;
        }

        public Name? Label { get; }
        public AnyType Type { get; }

        public static FunctionTypeArgument Parse(Parser parser)
        {
            if (parser.Next == ToKind.Colon)
            {
                var label = Name.Parse(parser);
                var annotation = TypeAnnotation.Parse(parser);

                return new FunctionTypeArgument(label, annotation);
            }

            var type = AnyType.Parse(parser);
            return new FunctionTypeArgument(null, type);

        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label} ";
            return $"{label}{Type}";
        }
    }
}
