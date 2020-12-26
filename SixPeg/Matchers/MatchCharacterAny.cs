﻿using Six.Support;
using SixPeg.Matches;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchCharacterAny : AnyMatcher
    {
        public MatchCharacterAny()
        {
        }

        public override bool IsClassy => true;

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            var next = start;
            if (InnerMatch(subject, ref next))
            {
                yield return IMatch.Success(this, before, start, next);
            }
            else
            {
                yield break;
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            if (cursor < subject.Length)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}match any");
        }

        public override string DDLong => $"any(.)";

        public override string Marker => ".";
    }
}
