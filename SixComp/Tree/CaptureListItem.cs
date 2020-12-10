using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class CaptureListItem
        {
            public CaptureListItem(CaptureSpecifier? specifier, IExpression expression)
            {
                Specifier = specifier;
                Expression = expression;
            }

            public CaptureSpecifier? Specifier { get; }
            public IExpression Expression { get; }

            public static CaptureListItem Parse(Parser parser)
            {
                var specifier = CaptureSpecifier.Parse(parser);
                var expression = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(CaptureListItem)}");

                return new CaptureListItem(specifier, expression);
            }
        }
    }
}