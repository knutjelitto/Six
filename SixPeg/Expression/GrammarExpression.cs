using Six.Support;
using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class GrammarExpression : AnyExpression
    {
        private readonly Dictionary<Identifier, RuleExpression> Indexed = new Dictionary<Identifier, RuleExpression>();

        public GrammarExpression(IList<RuleExpression> rules)
        {
            Rules = rules.ToList();
            Generated = new List<RuleExpression>();
        }

        public List<RuleExpression> Rules { get; }
        public List<RuleExpression> Generated { get; }
        public RuleExpression Start { get; private set; }
        public RuleExpression Space { get; private set; }
        public IMatcher Matcher { get; private set; }

        public bool Error { get; private set; } = false;

        private IWriter writer = null;

        public override IMatcher GetMatcher(bool spaced)
        {
            return Start.GetMatcher(spaced);
        }

        public void ReportMatchers(IWriter writer)
        {
            bool more = false;
            foreach (var rule in Rules)
            {
                if (more)
                {
                    writer.WriteLine();
                }
                using (writer.Indent($"rule {rule.Name}"))
                {
                    rule.GetMatcher(false).Write(writer);
                }
                more = true;
            }
        }

        public void Resolve(IWriter writer)
        {
            this.writer = writer;
            Resolve(this);
            this.writer = null;
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Start = null;

            foreach (var rule in Rules)
            {
                if (Indexed.TryGetValue(rule.Name, out var already))
                {
                    writer.WriteLine($"already defined rule: {already.Name}");
                    Error = true;
                }
                else
                {
                    Indexed.Add(rule.Name, rule);
                }

                if (rule.Name.Text == "_")
                {
                    Space = rule;
                }

                if (Start == null)
                {
                    Start = rule;
                    Start.Used = true;
                }
            }

            if (Start == null)
            {
                writer.WriteLine("nothing defined in here");
            }

            if (Space == null)
            {
                writer.WriteLine("no space rule defined");
                Error = true;
            }

            var i = 0;
            while (i < Rules.Count)
            {
                Rules[i].Resolve(this);
                i += 1;
            }

            Matcher = GetMatcher(false);

            foreach (var rule in Rules)
            {
                if (!rule.Used)
                {
                    writer.WriteLine($"unused rule: {rule.Name}");
                }
            }
        }

        public RuleExpression FindRule(Identifier name)
        {
            if (Indexed.TryGetValue(name, out var rule))
            {
                rule.Used = true;
                return rule;
            }
            else
            {
                writer.WriteLine($"undefined rule: {name}");
                Error = true;
                return null;
            }
        }

        public RuleExpression AddSpaced(SpacedExpression spaced)
        {
            var name = $"'{spaced.Text}'";
            var identifier = new Identifier(name);

            if (!Indexed.TryGetValue(identifier, out var rule))
            {
                var sequence = new SequenceExpression(new List<AnyExpression> { new NameExpression(Space.Name), new StringExpression(spaced.Text) });
                rule = new RuleExpression(identifier, sequence);
                Indexed.Add(identifier, rule);
                Rules.Add(rule);
            }

            return rule;
        }
    }
}
