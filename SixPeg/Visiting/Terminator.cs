using Six.Support;
using SixPeg.Matchers;

namespace SixPeg.Visiting
{
    public class Terminator : MatcherVisitor
    {
        private MatchRule Current;

        public Terminator(Parser parser)
        {
            Parser = parser;
        }

        public Parser Parser { get; }

        public void Terminate()
        {
            Parser.Start.UsedByRule = true;
            Parser.Space.UsedByRule = true;
            foreach (var rule in Parser.Rules)
            {
                Current = rule;
                _ = rule.Matcher.Accept(this);
            }
            foreach (var rule in Parser.Rules)
            {
                if (!rule.Used)
                {
                    new Error(rule.Name.Source).Report($"unused rule `{rule.Name}`", rule.Name.Start, rule.Name.Length);
                    throw new BailOutException();
                }
#if false
                if (!rule.IsTerminal && rule.UsedByTerminal)
                {
                    new Error(rule.Name.Source).Report($"nonterminal rule `{rule.Name}` is used by terminal", rule.Name.Start, rule.Name.Length);
                    throw new BailOutException();
                }
#endif
            }
        }

        public override bool Visit(MatchReference matcher)
        {
            matcher.Rule.UsedByTerminal = matcher.Rule.UsedByTerminal || Current.IsTerminal;
            matcher.Rule.UsedByRule = matcher.Rule.UsedByRule || !Current.IsTerminal;

            return true;
        }
    }
}
