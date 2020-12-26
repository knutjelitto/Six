﻿using Six.Support;
using SixPeg.Matches;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchCharacterRange : AnyMatcher
    {
        public MatchCharacterRange(char minCharacter, char maxCharacter)
        {
            MinCharacter = minCharacter;
            MaxCharacter = maxCharacter;
        }

        public char MinCharacter { get; }
        public char MaxCharacter { get; }

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
            if (cursor < subject.Length && MinCharacter <= subject.Text[cursor] && subject.Text[cursor] <= MaxCharacter)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}match \"{MinCharacter.Escape()}\" .. \"{MaxCharacter.Escape()}\"");
        }

        public override string DDLong => $"range(\"{MinCharacter.Escape()}\",\"{MaxCharacter.Escape()}\")";
        public override string Marker => "[..]";
    }
}
