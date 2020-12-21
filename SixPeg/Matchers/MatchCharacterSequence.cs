using Six.Support;
using System;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchCharacterSequence : AnyMatcher
    {
        public MatchCharacterSequence(string text)
        {
            Debug.Assert(text.Length >= 2);
            Text = text;
        }

        public string Text { get; }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            if (cursor + Text.Length <= subject.Length && MemoryExtensions.Equals(Text.AsSpan(), subject.Text.AsSpan(cursor, Text.Length), StringComparison.Ordinal))
            {
                cursor += Text.Length;
                return true;
            }

            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}match \"{Text.Escape()}\"");
        }

        public override string DDShort => $"string(\"{Text.Escape()}\")";
    }
}
