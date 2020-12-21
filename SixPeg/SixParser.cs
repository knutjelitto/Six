using Six.Support;
using SixPeg.Expression;
using System.Collections.Generic;

namespace SixPeg
{
    public partial class SixParser
    {
        public IEnumerable<RuleExpression> Parse(Source source)
        {
            return Parse(source.Text, source.Name);
        }
    }
}
