using Six.Support;
using SixPeg.Matchers;
using SixPeg.Visiting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Writing
{
    public class Emitter : IMatcherVisitor<bool>
    {
        private string NmSuccess => $"{D.NmMatchType}.Success";
        private string NmOptional => $"{D.NmMatchType}.Optional";
        private const string PmContext = "context";
        private const string TyContext = "Context";

        public Emitter(Parser parser, IWriter writer)
        {
            Parser = parser;
            Writer = writer;
            N = new Namer();
            D = new WriteDecl(Writer, N);
        }

        public Parser Parser { get; }
        public IWriter Writer { get; }
        public WriteDecl D { get; }
        public Namer N { get; }

        private readonly Stack<string> ResultStack = new Stack<string>();
        private readonly Stack<string> NextStack = new Stack<string>();
        public IDisposable SetResult(string local)
        {
            ResultStack.Push(local);
            return new Disposable(() => ResultStack.Pop());
        }
        public string GetResult() => ResultStack.Peek();
        public IDisposable SetStart(string local)
        {
            NextStack.Push(local);
            return new Disposable(() => NextStack.Pop());
        }
        public string GetStart() => NextStack.Peek();

        public void Emit()
        {
            D.Line("#if true");
            D.Line("using System;");
            D.Line("using System.Diagnostics;");
            D.Line("using System.Collections.Generic;");
            D.Line("using SixPeg.Runtime;");
            D.NL();
            using (D.Block($"namespace SixPeg.Pegger.{Parser.Name}"))
            {
                using (D.Block($"public abstract class {Parser.Name}Pegger : Runtime.Pegger"))
                {
                    D.Line($"public {Parser.Name}Pegger({TyContext} {PmContext})");
                    D.Line($"    : base({PmContext}, {Parser.Rules.Count})");
                    using (D.Block())
                    {
                    }

                    if (Parser.Keywords.Count > 0)
                    {
                        using (D.Block($"protected HashSet<string> _keywords = new HashSet<string>"))
                        {
                            foreach (var keyword in Parser.Keywords)
                            {
                                D.Line($"\"{keyword.Escape()}\",");
                            }
                        }
                        D.Line(";");
                    }

                    foreach (var rule in Parser.Rules)
                    {
                        D.NL();
                        _ = rule.Accept(this);
                    }
                }
            }
            D.Line("#endif");
        }

        public bool Visit(MatchRule rule)
        {
            N.Reset();

            if (rule.Fragment)
            {
                Debug.Assert(true);
            }

            var cacheIndexName = $"Cache_{N.NameFor(rule)}";
            if (!rule.Fragment)
            {
                D.Line($"protected const int {cacheIndexName} = {rule.Index};");
                D.NL();
            }
            using (D.Block($"public virtual {D.NmMatchType} {N.NameFor(rule)}(int start)"))
            {
                string match;
                using (SetStart("start"))
                {
                    if (rule.Fragment)
                    {
                        match = D.NewMatch();
                        using (SetResult(match))
                        {
                            _ = rule.Matcher.Accept(this);
                        }
                    }
                    else
                    {
                        match = N.Local(D.NmResult);
                        using (SetResult(match))
                        using (D.If($"!Caches[{cacheIndexName}].Already({GetStart()}, out var {match})"))
                        {
                            _ = rule.Matcher.Accept(this);
                            D.Line($"Caches[{cacheIndexName}].Cache({GetStart()}, {match});");
                        }
                    }
                }
                D.Line($"return {match};");

            }
            return true;
        }


        public bool Visit(MatchSequence sequence)
        {
            var next = D.NewVar(D.NmNext, GetStart());
            var matches = D.NewMatches();
            using (SetStart(next))
            using (D.Block("for (;;) // ---Sequence---"))
            {
                var last = sequence.Matchers.Last();
                foreach (var matcher in sequence.Matchers)
                {
                    var inline = Inline(matcher);
                    if (inline != null)
                    {
                        using (D.If($"({GetResult()} = {inline}) == null"))
                        {
                            D.Line("break;");
                        }
                    }
                    else
                    {
                        _ = matcher.Accept(this);
                        if (!((matcher is MatchZeroOrOne) || (matcher is MatchZeroOrMore)))
                        {
                            using (D.If($"{GetResult()} == null"))
                            {
                                D.Line("break;");
                            }
                        }
                    }
                    D.Line($"{matches}.Add({GetResult()});");
                    if (matcher != last)
                    {
                        D.Line($"{next} = {GetResult()}.Next;");
                    }
                }
                D.Line("break;");
            }
            using (D.If($"{GetResult()} != null"))
            {
                D.Line($"{GetResult()} = {NmSuccess}(start, {matches});");
            }

            return true;
        }

        public bool Visit(MatchChoice choice)
        {
            using (D.Block("for (;;) // ---Choice---"))
            {
                var last = choice.Matchers.Last();
                foreach (var matcher in choice.Matchers)
                {
                    var inline = Inline(matcher);
                    if (inline != null)
                    {
                        if (matcher == last)
                        {
                            D.Line($"{GetResult()} = {inline};");
                            D.Line("break;");
                        }
                        else
                        {
                            using (D.If($"({GetResult()} = {inline}) != null"))
                            {
                                D.Line("break;");
                            }
                        }
                    }
                    else
                    {
                        _ = matcher.Accept(this);
                        if (matcher == last)
                        {
                            D.Line("break;");
                        }
                        else
                        {
                            using (D.If($"{GetResult()} != null"))
                            {
                                D.Line("break;");
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool Visit(MatchAnd matcher)
        {
            using (D.Indent("And"))
            {
                var inline = Inline(matcher.Matcher);
                if (inline != null)
                {
                    D.Line($"{GetResult()} = {inline};");
                }
                else
                {
                    _ = matcher.Matcher.Accept(this);
                    D.Line($"{GetResult()} = {nameof(Runtime.Pegger.And_)}({GetStart()}, {GetResult()});");
                }
            }
            return true;
        }

        public bool Visit(MatchNot matcher)
        {
            using (D.Indent("Not"))
            {
                var inline = Inline(matcher.Matcher);
                if (inline != null)
                {
                    D.Line($"{GetResult()} = {inline};");
                }
                else
                {
                    _ = matcher.Matcher.Accept(this);
                    D.Line($"{GetResult()} = {nameof(Runtime.Pegger.Not_)}({GetStart()}, {GetResult()});");
                }
            }
            return true;
        }

        public bool Visit(MatchBefore matcher)
        {
            using (D.Indent("Before"))
            {
                D.Line($"throw new NotImplementedException();");
            }
            return false;
        }

        public bool Visit(MatchCharacterAny matcher)
        {
            D.Line($"{GetResult()} = {Inline(matcher)});");
            return true;
        }

        public bool Visit(MatchCharacterExact matcher)
        {
            D.Line($"{GetResult()} = {Inline(matcher)};");
            return true;
        }

        public bool Visit(MatchCharacterRange matcher)
        {
            D.Line($"{GetResult()} = {Inline(matcher)};");
            return true;
        }

        public bool Visit(MatchCharacterSequence matcher)
        {
            D.Line($"{GetResult()} = {Inline(matcher)};");
            return true;
        }

        public bool Visit(MatchCharacterSet matcher)
        {
            D.Line($"{GetResult()} = {Inline(matcher)};");
            return true;
        }

        public bool Visit(MatchReference matcher)
        {
            D.Line($"{GetResult()} = {Inline(matcher)};");
            return true;
        }

        public bool Visit(MatchEpsilon matcher)
        {
            using (D.Indent("Epsilon"))
            {
                D.Line($"{GetResult()} = {NmSuccess}({GetStart()});");
            }
            return true;
        }

        public bool Visit(MatchError matcher)
        {
            using (D.Indent("Error"))
            {
                D.Line($"throw new NotImplementedException();");
            }
            return true;
        }

        public bool Visit(MatchOneOrMore matcher)
        {
            using (D.Indent("OneOrMore"))
            {
                var matches = D.NewMatches(D.NmOomResults);
                var next = D.NewVar(D.NmOomNext, GetStart());
                using (SetStart(next))
                using (D.Block($"for (;;)"))
                {
                    var inline = Inline(matcher.Matcher);
                    if (inline != null)
                    {
                        using (D.If($"({GetResult()} = {inline}) == null"))
                        {
                            D.Line("break;");
                        }
                    }
                    else
                    {
                        _ = matcher.Matcher.Accept(this);
                        using (D.If($"{GetResult()} == null"))
                        {
                            D.Line("break;");
                        }
                    }
                    D.Line($"{matches}.Add({GetResult()});");
                    D.Line($"{next} = {GetResult()}.Next;");
                }
                using (D.If($"{matches}.Count > 0"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {matches});");
                }
            }
            return false;
        }

        public bool Visit(MatchZeroOrMore matcher)
        {
            using (D.Indent("ZeroOrMore"))
            {
                var matches = D.NewMatches(D.NmZomResults);
                var next = D.NewVar(D.NmZomNext, GetStart());
                using (SetStart(next))
                using (D.Block($"for (;;)"))
                {
                    var inline = Inline(matcher.Matcher);
                    if (inline != null)
                    {
                        using (D.If($"({GetResult()} = {inline}) == null"))
                        {
                            D.Line("break;");
                        }
                    }
                    else
                    {
                        _ = matcher.Matcher.Accept(this);
                        using (D.If($"{GetResult()} == null"))
                        {
                            D.Line("break;");
                        }
                    }
                    D.Line($"{matches}.Add({GetResult()});");
                    D.Line($"{next} = {GetResult()}.Next;");
                }
                D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {matches});");
            }
            return true;
        }

        public bool Visit(MatchZeroOrOne matcher)
        {
            var inline = Inline(matcher.Matcher);
            if (inline != null)
            {
                D.Line($"{GetResult()} = {NmOptional}({GetStart()}, {inline});");
            }
            else
            {
                _ = matcher.Matcher.Accept(this);
                D.Line($"{GetResult()} = {NmOptional}({GetStart()}, {GetResult()});");
            }
            return true;
        }

        private string Inline(AnyMatcher any)
        {
            if (any is MatchReference reference)
            {
                return $"{N.NameFor(reference.Rule)}({GetStart()})";
            }
            if (any is MatchCharacterSequence sequence)
            {
                return $"{nameof(Runtime.Pegger.CharacterSequence_)}({GetStart()}, \"{sequence.Text.Escape()}\")";
            }
            if (any is MatchCharacterAny)
            {
                return $"{nameof(Runtime.Pegger.CharacterAny_)}({GetStart()})";
            }
            if (any is MatchCharacterExact exact)
            {
                return $"{nameof(Runtime.Pegger.CharacterExact_)}({GetStart()}, '{exact.Character.Escape()}')";
            }
            if (any is MatchCharacterRange range)
            {
                return $"{nameof(Runtime.Pegger.CharacterRange_)}({GetStart()}, '{range.MinCharacter.Escape()}', '{range.MaxCharacter.Escape()}')";
            }
            if (any is MatchCharacterSet set)
            {
                return $"{nameof(Runtime.Pegger.CharacterSet_)}({GetStart()}, \"{set.Set.Escape()}\")";
            }
            if (any is MatchNot not)
            {
                var inline = Inline(not.Matcher);
                if (inline != null)
                {
                    return $"{nameof(Runtime.Pegger.Not_)}({GetStart()}, {inline})";
                }
            }
            if (any is MatchAnd and)
            {
                var inline = Inline(and.Matcher);
                if (inline != null)
                {
                    return $"{nameof(Runtime.Pegger.And_)}({GetStart()}, {inline})";
                }
            }
            return null;
        }
    }
}
