using Six.Peg.Runtime;

namespace Six.Comp
{
    public class Parser : SixPeg.Pegger.Swift.SwiftPegger
    {
        public Parser(Context context)
            : base(context)
        {
        }

        public override Match Name(int start)
        {
            if (!Caches[Cache_Name].Already(start, out var match))
            {
                var space = _(start);
                var id = Identifier(space.Next);
                if (id != null)
                {
                    match = Match.Success("Name", id);
                    match.Before = start;
                }
                else
                {
                    match = null;
                }

                Caches[Cache_Name].Cache(start, match);
            }
            return match;
        }

        public override Match More(int start)
        {
            if (start < ContextLength)
            {
                var ch = ContextText[start];
                if (char.IsLetterOrDigit(ch) || ch == '_')
                {
                    return Match.Success("More", start, start + 1);
                }
            }
            return null;
        }

#if true
        public override Match _(int start)
        {
            if (!Caches[Cache__].Already(start, out var match))
            {
                var current = start;
                while (Whitespace(ref current))
                {
                }
                match = Match.Success("SPACE", start, current);
                Caches[Cache__].Cache(start, match);
            }
            return match;
        }
#else
        public override Match _(int start)
        {
            if (!Caches[Cache__].Already(start, out var match))
            {
                var next = start;
                while ((match = Whitespace(next)) != null)
                {
                    next = match.Next;
                }
                match = Match.Success("SPACE", start, next);
                Caches[Cache__].Cache(start, match);
            }
            return match;
        }
#endif

        private bool Whitespace(ref int current)
        {
            var start = current;
            while (current < ContextLength)
            {
                switch (ContextText[current])
                {
                    case '\u0000':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0020':
                        current += 1;
                        break;
                    case '/':
                        if (current + 1 < ContextLength)
                        {
                            if (ContextText[current + 1] == '/')
                            {
                                current += 2;
                                while (current < ContextLength && ContextText[current] != '\u000A' && ContextText[current] != '\u000D')
                                {
                                    current += 1;
                                }
                            }
                            else if (ContextText[current + 1] == '*')
                            {
                                MultiLineComment(ref current);
                            }
                            else
                            {
                                return current > start;
                            }
                        }
                        break;
                    default:
                        return current > start;
                }
            }

            return false;

            void MultiLineComment(ref int current)
            {
                current += 2;
                while (current + 1 < ContextLength)
                {
                    if (ContextText[current] == '*' && ContextText[current + 1] == '/')
                    {
                        current += 2;
                        return;
                    }
                    if (ContextText[current] == '/' && ContextText[current + 1] == '*')
                    {
                        MultiLineComment(ref current);
                    }
                    else
                    {
                        current += 1;
                    }
                }

                new Error(Context).Report("unterminated multiline comment at EOF", current);
                throw new BailOutException();
            }
        }

        public override Match Whitespace(int start)
        {
            var current = start;
            while (current < ContextLength)
            {
                switch (ContextText[current])
                {
                    case '\u0000':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0020':
                        current += 1;
                        break;
                    case '/':
                        if (current + 1 < ContextLength)
                        {
                            if (ContextText[current + 1] == '/')
                            {
                                SingleLineComment();
                            }
                            else if (ContextText[current + 1] == '*')
                            {
                                MultiLineComment();
                            }
                            else
                            {
                                goto okorerror;
                            }
                        }
                        break;
                    default:
                        goto okorerror;
                }
            }

        okorerror:
            if (current > start)
            {
                return Match.Success("Whitespace", start, current);
            }
            return null;


            void SingleLineComment()
            {
                current += 2;
                while (current < ContextLength && ContextText[current] != '\u000A' && ContextText[current] != '\u000D')
                {
                    current += 1;
                }
            }

            void MultiLineComment()
            {
                current += 2;
                while (current + 1 < ContextLength)
                {
                    if (ContextText[current] == '*' && ContextText[current + 1] == '/')
                    {
                        current += 2;
                        return;
                    }
                    if (ContextText[current] == '/' && ContextText[current + 1] == '*')
                    {
                        MultiLineComment();
                    }
                    else
                    {
                        current += 1;
                    }
                }

                new Error(Context).Report("unterminated multiline comment at EOF", current);
                throw new BailOutException();
            }
        }
    }
}
