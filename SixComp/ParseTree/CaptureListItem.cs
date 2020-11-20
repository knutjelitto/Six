﻿namespace SixComp.ParseTree
{
    public class CaptureListItem
    {
        public CaptureListItem(CaptureSpecifier? specifier, AnyExpression expression)
        {
            Specifier = specifier;
            Expression = expression;
        }

        public CaptureSpecifier? Specifier { get; }
        public AnyExpression Expression { get; }

        public static CaptureListItem Parse(Parser parser)
        {
            var specifier = CaptureSpecifier.Parse(parser);
            var expression = AnyExpression.Parse(parser);

            return new CaptureListItem(specifier, expression);
        }
    }
}
