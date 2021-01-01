using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class Grammar
    {
        public Grammar(IList<AnyRule> rules, IList<OptionExpression> options)
        {
            Rules = rules.ToList();
            Options = options.ToList();
        }

        public List<AnyRule> Rules { get; }
        public List<OptionExpression> Options { get; }
        public bool Error { get; set; } = false;
    }
}
