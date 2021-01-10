using Six.Support;
using SixPeg.Matchers;
using SixPeg.Visiting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Writing
{
    public static class EmitterExtensions
    {
        public static void Emit<T>(this T visitable, Emitter visitor)
            where T : IMatcher
        {
            _ = visitable.Accept(visitor);
        }

        public static IEnumerable<AnyMatcher> Transform(this IEnumerable<AnyMatcher> matchers)
        {
            return matchers.Select(m => m.Transform());
        }

        public static AnyMatcher Transform(this AnyMatcher matcher)
        {
            if (matcher is MatchReference reference && reference.Rule.IsFragment)
            {
                return reference.Rule.Matcher.Transform();
            }
            return matcher;
        }
    }

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
            //D.Line("using System;");
            //D.Line("using System.Diagnostics;");
            D.Line("using System.Collections.Generic;");
            D.Line("using Six.Peg.Runtime;");
            D.NL();
            using (D.Block($"namespace SixPeg.Pegger.{Parser.Name}"))
            {
                using (D.Block($"public abstract class {Parser.Name}Pegger : Six.Peg.Runtime.Pegger"))
                {
                    D.Line($"public {Parser.Name}Pegger({TyContext} {PmContext})");
                    D.Line($"    : base({PmContext}, {Parser.Rules.Count})");
                    using (D.Block())
                    {
                    }


                    foreach (var rule in Parser.Rules)
                    {
                        D.NL();
                        rule.Emit(this);
                    }

                    D.NL();
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

            var ruleName = N.NameFor(rule);

            var cacheIndexName = $"Cache_{ruleName}";
            if (!rule.Fragment)
            {
                D.Line($"protected const int {cacheIndexName} = {rule.Index};");
                D.NL();
            }
            using (D.Block($"public virtual {D.NmMatchType} {ruleName}(int start)"))
            {
                string match;
                using (SetStart("start"))
                {
                    if (rule.Fragment)
                    {
                        match = D.NewMatch();
                        using (SetResult(match))
                        {
                            rule.Matcher.Transform().Emit(this);
                            using (D.If($"{GetResult()} != null"))
                            {
                                D.Line($"{GetResult()} = {NmSuccess}({ruleName.AsStringLiteral()}, {GetStart()}, {GetResult()});");
                            }
                        }
                    }
                    else
                    {
                        match = N.Local(D.NmResult);
                        using (SetResult(match))
                        using (D.If($"!Caches[{cacheIndexName}].Already({GetStart()}, out var {match})"))
                        {
                            rule.Matcher.Transform().Emit(this);
                            if (rule == Parser.Space)
                            {
                                D.Line($"{GetResult()} = {NmSuccess}(\"SPACE\", {GetStart()}, {GetResult()}.Next);");
                            }
                            else
                            {
                                using (D.If($"{GetResult()} != null"))
                                {
                                    D.Line($"{GetResult()} = {NmSuccess}({ruleName.AsStringLiteral()}, {GetStart()}, {GetResult()});");
                                }
                            }
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
            using (D.Block("while (true) // ---Sequence---"))
            {
                var matchers = sequence.Matchers.Transform();

                var last = matchers.Last();
                var bailout = false;
                foreach (var matcher in matchers)
                {
                    var inline = Inline(matcher);
                    if (inline != null)
                    {
                        if (matcher.AlwaysSucceeds)
                        {
                            D.Line($"{GetResult()} = {inline};");
                        }
                        else
                        {
                            using (D.If($"({GetResult()} = {inline}) == null"))
                            {
                                D.Line("break;");
                            }
                        }
                    }
                    else
                    {
                        matcher.Emit(this);
                        if (matcher.NeverContinues)
                        {
                            bailout = true;
                            break;
                        }
                        if (!matcher.AlwaysSucceeds)
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
                if (!bailout)
                {
                    D.Line("break;");
                }
            }
            using (D.If($"{GetResult()} != null"))
            {
                D.Line($"{GetResult()} = {NmSuccess}({sequence.Marker.AsStringLiteral()}, {GetStart()}, {matches});");
            }

            return true;
        }

        public bool Visit(MatchChoice choice)
        {
            using (D.Block("while (true) // ---Choice---"))
            {
                var matchers = choice.Matchers.Transform().ToList();
                var last = matchers.Last();
                foreach (var matcher in matchers)
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
                        matcher.Emit(this);
                        if (!matcher.NeverContinues)
                        {
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
            }

            return true;
        }

        public bool Visit(MatchAnd matcher)
        {
            var inline = Inline(matcher.Matcher.Transform());
            if (inline != null)
            {
                D.Line($"{GetResult()} = {inline};");
            }
            else
            {
                matcher.Matcher.Transform().Emit(this);
                D.Line($"{GetResult()} = {nameof(Six.Peg.Runtime.Pegger.And_)}({GetStart()}, {GetResult()});");
            }

            return true;
        }

        public bool Visit(MatchNot not)
        {
            var inline = Inline(not.Matcher.Transform());
            if (inline != null)
            {
                D.Line($"{GetResult()} = {inline};");
            }
            else
            {
                not.Matcher.Transform().Emit(this);
                D.Line($"{GetResult()} = {nameof(Six.Peg.Runtime.Pegger.Not_)}({GetStart()}, {GetResult()});");
            }

            return true;
        }

        public bool Visit(MatchBefore matcher)
        {
            D.Line($"{GetResult()} = null;");
            using (D.If($"{GetStart()} - 1 >= 0"))
            {
                using (SetStart($"{GetStart()} - 1"))
                {
                    matcher.Matcher.Emit(this);
                }
                using (D.If($"{GetResult()} != null"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({matcher.Marker.AsStringLiteral()}, {GetStart()});");
                }
            }

            return true;
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
            D.Line($"{GetResult()} = {NmSuccess}({matcher.Marker.AsStringLiteral()}, {GetStart()});");

            return true;
        }

        public bool Visit(MatchError matcher)
        {
            using (D.Block())
            {
                D.Line("// >>> ERROR");
                var arguments = string.Join(" ", matcher.Arguments);
                D.Line($"new Error(Context).Report(\"{arguments}\", {GetStart()});");
                D.Line($"throw new BailOutException();");
                D.Line("// <<< ERROR");
            }
            return true;
        }

        public bool Visit(MatchOneOrMore oneOrMore)
        {
            var matches = D.NewMatches(D.NmOomResults);
            var next = D.NewVar(D.NmOomNext, GetStart());
            using (SetStart(next))
            using (D.Block($"while (true)"))
            {
                var matcher = oneOrMore.Matcher.Transform();

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
                    matcher.Emit(this);
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
                D.Line($"{GetResult()} = {NmSuccess}({oneOrMore.Marker.AsStringLiteral()}, {GetStart()}, {matches});");
            }

            return true;
        }

        public bool Visit(MatchZeroOrMore zeroOrMore)
        {
            var matches = D.NewMatches(D.NmZomResults);
            var next = D.NewVar(D.NmZomNext, GetStart());
            using (SetStart(next))
            using (D.Block($"while (true)"))
            {
                var matcher = zeroOrMore.Matcher.Transform();

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
                    matcher.Emit(this);
                    using (D.If($"{GetResult()} == null"))
                    {
                        D.Line("break;");
                    }
                }
                D.Line($"{matches}.Add({GetResult()});");
                D.Line($"{next} = {GetResult()}.Next;");
            }
            D.Line($"{GetResult()} = {NmSuccess}({zeroOrMore.Marker.AsStringLiteral()}, {GetStart()}, {matches});");

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
                matcher.Matcher.Emit(this);
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
                return $"{nameof(Six.Peg.Runtime.Pegger.CharacterSequence_)}({GetStart()}, \"{sequence.Text.Escape()}\")";
            }
            if (any is MatchCharacterAny)
            {
                return $"{nameof(Six.Peg.Runtime.Pegger.CharacterAny_)}({GetStart()})";
            }
            if (any is MatchCharacterExact exact)
            {
                return $"{nameof(Six.Peg.Runtime.Pegger.CharacterExact_)}({GetStart()}, '{exact.Character.Escape()}')";
            }
            if (any is MatchCharacterRange range)
            {
                return $"{nameof(Six.Peg.Runtime.Pegger.CharacterRange_)}({GetStart()}, '{range.MinCharacter.Escape()}', '{range.MaxCharacter.Escape()}')";
            }
            if (any is MatchCharacterSet set)
            {
                return $"{nameof(Six.Peg.Runtime.Pegger.CharacterSet_)}({GetStart()}, \"{set.Set.Escape()}\")";
            }
            if (any is MatchNot not)
            {
                var inline = Inline(not.Matcher);
                if (inline != null)
                {
                    return $"{nameof(Six.Peg.Runtime.Pegger.Not_)}({GetStart()}, {inline})";
                }
            }
            if (any is MatchAnd and)
            {
                var inline = Inline(and.Matcher);
                if (inline != null)
                {
                    return $"{nameof(Six.Peg.Runtime.Pegger.And_)}({GetStart()}, {inline})";
                }
            }
            return null;
        }
    }
}
