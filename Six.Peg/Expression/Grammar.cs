using System.Collections.Generic;
using System.Linq;

namespace Six.Peg.Expression
{
    public class Grammar
    {
        public Grammar(IList<Rule> rules, IList<OptionExpression> options)
        {
            Rules = rules.ToList();
            Options = options.ToList();
        }

        public List<Rule> Rules { get; }
        public List<OptionExpression> Options { get; }
        public bool Error { get; set; } = false;
    }
}
