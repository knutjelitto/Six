using Six.Support;
using System;

namespace SixPeg.Matchers
{
    public class MatchCharacterSequence : AnyMatcher
    {
        public MatchCharacterSequence(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public override bool Match(string subject, ref int cursor)
        {
            if (cursor + Text.Length < subject.Length && MemoryExtensions.Equals(Text.AsSpan(), subject.AsSpan(cursor, Text.Length), StringComparison.Ordinal))
            {
                cursor += Text.Length;
                return true;
            }

            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"match \"{Text}\"");
        }
    }
}
