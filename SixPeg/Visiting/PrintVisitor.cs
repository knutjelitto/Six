using Six.Support;
using SixPeg.Matchers;
using System.Linq;

namespace SixPeg.Visiting
{
    public class PrintVisitor : IMatcherVisitor<bool>
    {
        public PrintVisitor(IWriter writer)
        {
            Writer = writer;
        }

        public IWriter Writer { get; }

        public void Print(Parser parser)
        {
            using (Writer.Indent("rules:"))
            {
                foreach (var matcher in parser.Rules)
                {
                    _ = matcher.Accept(this);
                }
            }
            Writer.WriteLine();
            using (Writer.Indent("keywords:"))
            {
                foreach (var keyword in parser.Keywords.OrderBy(k => k))
                {
                    Writer.WriteLine($"{keyword}");
                }
            }
        }

        public bool Visit(MatchRule matcher)
        {
            var rt = matcher.IsTerminal ? "T" : "R";
            var f = matcher.Fragment ? "F" : "";

            using (Writer.Indent($"{matcher.Name}[{rt}{f}]:"))
            {
                return matcher.Matcher.Accept(this);
            }
        }

        public bool Visit(MatchReference matcher)
        {
            Writer.WriteLine($"{matcher.SpacePrefix}{matcher.Rule.Name}");

            return true;
        }

        public bool Visit(MatchChoice matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}choice"))
            {
                foreach (var m in matcher.Matchers)
                {
                    _ = m.Accept(this);
                }
            }
            return true;
        }

        public bool Visit(MatchSequence matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}sequence"))
            {
                foreach (var m in matcher.Matchers)
                {
                    _ = m.Accept(this);
                }
            }
            return true;
        }

        public bool Visit(MatchNot matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}not"))
            {
                return matcher.Matcher.Accept(this);
            }
        }

        public bool Visit(MatchAnd matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}and"))
            {
                return matcher.Matcher.Accept(this);
            }
        }

        public bool Visit(MatchBefore matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}before"))
            {
                return matcher.Matcher.Accept(this);
            }
        }

        public bool Visit(MatchCharacterAny matcher)
        {
            Writer.WriteLine($"{matcher.SpacePrefix}match ANY");

            return true;
        }

        public bool Visit(MatchCharacterExact matcher)
        {
            Writer.WriteLine($"{matcher.SpacePrefix}match \"{matcher.Character.Escape()}\"");

            return true;
        }

        public bool Visit(MatchCharacterRange matcher)
        {
            Writer.WriteLine($"{matcher.SpacePrefix}match \"{matcher.MinCharacter.Escape()}\" .. \"{matcher.MaxCharacter.Escape()}\"");

            return true;
        }

        public bool Visit(MatchCharacterSequence matcher)
        {
            Writer.WriteLine($"{matcher.SpacePrefix}match \"{matcher.Text.Escape()}\"");

            return true;
        }

        public bool Visit(MatchCharacterSet matcher)
        {
            var set = string.Join("' or '", matcher.Set.Select(s => s.ToString().Escape()));

            Writer.WriteLine($"{matcher.SpacePrefix}match \'{set}\'");

            return true;
        }

        public bool Visit(MatchEpsilon matcher)
        {
            Writer.WriteLine($"{matcher.SpacePrefix}epsilon");

            return true;
        }

        public bool Visit(MatchError matcher)
        {
            Writer.WriteLine($"#error");

            return true;
        }

        public bool Visit(MatchOneOrMore matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}+"))
            {
                return matcher.Matcher.Accept(this);
            }
        }

        public bool Visit(MatchZeroOrMore matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}*"))
            {
                return matcher.Matcher.Accept(this);
            }
        }

        public bool Visit(MatchZeroOrOne matcher)
        {
            using (Writer.Indent($"{matcher.SpacePrefix}?"))
            {
                return matcher.Matcher.Accept(this);
            }
        }
    }
}
