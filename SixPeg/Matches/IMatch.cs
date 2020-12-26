using SixPeg.Matchers;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matches
{
    public interface IMatch
    {
        IMatcher Matcher { get; }
        int Before { get; }
        int Start { get; }
        int Next { get; }
        IReadOnlyList<IMatch> Matches { get; }


        public static readonly IReadOnlyList<IMatch> NoMatches = new IMatch[] { };

        [DebuggerStepThrough]
        public static IMatch Success(IMatcher matcher, int before, int start, int next, IReadOnlyList<IMatch> matches)
        {
            return new Match(matcher, before, start, next, matches);
        }

        [DebuggerStepThrough]
        public static IMatch Success(IMatcher matcher, int before, int start)
        {
            return new Match(matcher, before, start, start, NoMatches);
        }

        [DebuggerStepThrough]
        public static IMatch Success(IMatcher matcher, int before, int start, int next)
        {
            return new Match(matcher, before, start, next, NoMatches);
        }

        [DebuggerStepThrough]
        public static IMatch Success(IMatcher matcher, int before, int start, IMatch match)
        {
            return new Match(matcher, before, start, match.Next, new IMatch[] { match });
        }


        public static bool Differ(IMatch m1, IMatch m2)
        {
            if (m1.Matcher != m2.Matcher)
            {
                return true;
            }
            if (m1.Before != m2.Before)
            {
                return true;
            }
            if (m1.Start != m2.Start)
            {
                return true;
            }
            if (m1.Next != m2.Next)
            {
                return true;
            }
            if (m1.Matches.Count != m2.Matches.Count)
            {
                return true;
            }
            for (var i = 0; i < m1.Matches.Count; i += 1)
            {
                if (Differ(m1.Matches[i], m2.Matches[i]))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
