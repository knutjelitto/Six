using Six.Support;
using SixPeg.Matchers;
using SixPeg.Visiting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace SixPeg.Writing
{
    public class Emitter : IMatcherVisitor<bool>
    {
        private string NmSuccess => $"{D.NmMatch}.Success";
        private string NmOptional => $"{D.NmMatch}.Optional";
        private const string PmContext = "context";
        private const string NmContext = "Context";
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
            D.Line("using System.Collections.Generic;");
            D.Line("using SixPeg.Runtime;");
            D.NL();
            using (D.Block($"namespace SixPeg.Pegger.{Parser.Name}"))
            {
                using (D.Block($"public class {Parser.Name}Peg"))
                {
                    using (D.Block($"public {Parser.Name}Peg({TyContext} {PmContext})"))
                    {
                        D.Line($"{NmContext} = {PmContext};");
                    }
                    D.NL();
                    D.Line($"public {TyContext} {NmContext} {{ get; }}");

                    foreach (var rule in Parser.Rules)
                    {
                        D.NL();
                        _ = rule.Accept(this);
                    }
                }
            }
            D.Line("#endif");
        }

        public bool Visit(MatchRule matcher)
        {
            using (D.Block($"public {D.NmMatch} {N.NameFor(matcher)}(int start)"))
            {
                N.Reset();
                var match = D.NewMatch();
                using (SetResult(match))
                using (SetStart("start"))
                {
                    _ = matcher.Matcher.Accept(this);
                }
                D.Line($"return {match};");

            }
            return true;
        }


        public bool Visit(MatchSequence sequence)
        {
            using (D.Indent("Sequence"))
            {
                var next = D.NewVar("next", GetStart());
                var matches = D.NewMatches();
                var match = D.NewMatch();
                using (SetStart(next))
                using (SetResult(match))
                using (D.Block("for (;;)"))
                {
                    var last = sequence.Matchers.Last();
                    foreach (var matcher in sequence.Matchers)
                    {
                        using (SetResult(match))
                        {
                            _ = matcher.Accept(this);
                        }
                        using (D.If($"{match} == null"))
                        {
                            D.Line("break;");
                        }
                        D.Line($"{matches}.Add({match});");
                        if (matcher != last)
                        {
                            D.Line($"{next} = {match}.Next;");
                        }
                    }
                    D.Line("break;");
                }
                using (D.If($"{matches}.Count == {sequence.Matchers.Count}"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}(start, {string.Join(", ", matches)});");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }

            return true;
        }
#if false
        public bool Visit(MatchSequence matcher)
        {
            using (D.Indent("Sequence"))
            {
                D.Line($"{GetResult()} = null;");
                Emit(0, Enumerable.Empty<string>());
            }

            return true;

            void Emit(int index, IEnumerable<string> results)
            {
                if (index < matcher.Matchers.Count)
                {
                    var match = D.NewMatch();
                    using (SetResult(match))
                    {
                        _ = matcher.Matchers[index].Accept(this);
                    }
                    using (SetStart($"{match}.Next"))
                    {
                        using (D.If($"{match} != null"))
                        {
                            Emit(index + 1, results.Append(match));
                        }
                    }
                }
                else
                {
                    var matches = string.Join(", ", results);
                    D.Line($"{GetResult()} = {NmSuccess}(start, {matches});");
                }
            }
        }
#endif

        public bool Visit(MatchChoice matcher)
        {
            var match = D.NewMatch();

            using (D.Indent("Choice"))
            {
                D.Line($"{GetResult()} = null;");
                Emit(0);
            }

            return true;

            void Emit(int index)
            {
                Debug.Assert(index < matcher.Matchers.Count);
                using (SetResult(match))
                {
                    _ = matcher.Matchers[index].Accept(this);
                }
                using (D.If($"{match} != null"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {match});");
                }
                index += 1;
                if (index < matcher.Matchers.Count)
                {
                    using (D.Else())
                    {
                        Emit(index);
                    }
                }
            }
        }

        public bool Visit(MatchAnd matcher)
        {
            using (D.Indent("And"))
            {
                var match = D.NewMatch();
                using (SetResult(match))
                {
                    _ = matcher.Matcher.Accept(this);
                }
                using (D.If($"{match} != null"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()});");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
            return true;
        }

        public bool Visit(MatchNot matcher)
        {
            using (D.Indent("Not"))
            {
                var match = D.NewMatch();
                using (SetResult(match))
                {
                    _ = matcher.Matcher.Accept(this);
                }
                using (D.If($"{match} == null"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()});");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
            return true;
        }

        public bool Visit(MatchBefore matcher)
        {
            using (D.Indent("Before"))
            {
                throw new NotImplementedException();
            }
        }

        public bool Visit(MatchCharacterAny matcher)
        {
            using (D.Indent("CharacterAny"))
            {
                using (D.If($"{GetStart()} < {NmContext}.Length"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {GetStart()} + 1);");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
            return true;
        }

        public bool Visit(MatchCharacterExact matcher)
        {
            using (D.Indent("CharacterExact"))
            {
                using (D.If($"{GetStart()} < {NmContext}.Length && {NmContext}.Text[{GetStart()}] == '{matcher.Character.Escape()}'"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {GetStart()} + 1);");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
            return true;
        }

        public bool Visit(MatchCharacterRange matcher)
        {
            using (D.Indent("CharacterRange"))
            {
                using (D.If($"{GetStart()} < {NmContext}.Length && '{matcher.MinCharacter.Escape()}' <= {NmContext}.Text[{GetStart()}] && {NmContext}.Text[{GetStart()}] <= '{matcher.MaxCharacter.Escape()}'"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {GetStart()} + 1);");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
            return true;
        }

        public bool Visit(MatchCharacterSequence matcher)
        {
            using (D.Indent("CharacterSequence"))
            {
                using (D.If($"{GetStart()} + {matcher.Text.Length} <= {NmContext}.Length && MemoryExtensions.Equals(\"{matcher.Text.Escape()}\".AsSpan(), {NmContext}.Text.AsSpan({GetStart()}, {matcher.Text.Length}), StringComparison.Ordinal)"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {GetStart()} + {matcher.Text.Length});");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
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
                throw new NotImplementedException();
            }
        }

        private string Inline(MatchReference matcher)
        {
            return $"{N.NameFor(matcher.Rule)}({GetStart()})";
        }

        public bool Visit(MatchReference matcher)
        {
            using (D.Indent("Reference"))
            {
                D.Line($"{GetResult()} = {N.NameFor(matcher.Rule)}({GetStart()});");
            }
            return true;
        }

        public bool Visit(MatchOneOrMore matcher)
        {
            using (D.Indent("OneOrMore"))
            {
                var matches = D.NewMatches();
                var match = D.NewMatch();
                var next = D.NewVar("next", GetStart());
                using (D.Block($"for(;;)"))
                {
                    using (SetResult(match))
                    using (SetStart(next))
                    {
                        _ = matcher.Matcher.Accept(this);
                        using (D.If($"{match} == null"))
                        {
                            D.Line("break;");
                        }
                        using (D.Else())
                        {
                            D.Line($"{matches}.Add({match});");
                            D.Line($"{next} = {match}.Next;");
                        }
                    }
                }
                using (D.If($"{matches}.Count > 0"))
                {
                    D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {matches});");
                }
                using (D.Else())
                {
                    D.Line($"{GetResult()} = null;");
                }
            }
            return false;
        }

        public bool Visit(MatchZeroOrMore matcher)
        {
            using (D.Indent("ZeroOrMore"))
            {
                var matches = D.NewMatches();
                var match = D.NewMatch();
                var next = D.NewVar("next", GetStart());
                using (D.Block($"for(;;)"))
                {
                    using (SetResult(match))
                    using (SetStart(next))
                    {
                        _ = matcher.Matcher.Accept(this);
                        using (D.If($"{match} == null"))
                        {
                            D.Line("break;");
                        }
                        using (D.Else())
                        {
                            D.Line($"{matches}.Add({match});");
                            D.Line($"{next} = {match}.Next;");
                        }
                    }
                }
                D.Line($"{GetResult()} = {NmSuccess}({GetStart()}, {matches});");
            }
            return false;
        }

        public bool Visit(MatchZeroOrOne matcher)
        {
            using (D.Indent("ZeroOrOne"))
            {
                if (matcher.Matcher is MatchReference reference)
                {
                    D.Line($"{GetResult()} = {NmOptional}({GetStart()}, {Inline(reference)});");
                }
                else
                {
                    _ = matcher.Matcher.Accept(this);
                    D.Line($"{GetResult()} = {NmOptional}({GetStart()}, {GetResult()});");
                }
            }
            return true;
        }
    }
}
