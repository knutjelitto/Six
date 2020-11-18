﻿namespace SixComp.ParseTree
{
    public class TypeAnnotation : AnyType
    {
        private TypeAnnotation(PrefixedType type)
        {
            Type = type;
        }

        public PrefixedType Type { get; }

        public static TypeAnnotation Parse(Parser parser)
        {
            parser.Consume(ToKind.Colon);
            var type = PrefixedType.Parse(parser);

            return new TypeAnnotation(type);
        }

        public override string ToString()
        {
            return $": {Type}";
        }
    }
}
