using System.Collections.Generic;

namespace SixPeg.Expression
{
    public class Options : Grules<OptionExpression>
    {
        public Options() { }
        public Options(IEnumerable<OptionExpression> options) : base(options) { }
    }
}
