using System.Linq;

namespace Six.Support
{
    public static class CharacterExtensions
    {
        public static string Escape(this char character)
        {
            switch (character)
            {
                case '\b':
                    return "\\b";
                case '\f':
                    return "\\f";
                case '\n':
                    return "\\n";
                case '\r':
                    return "\\r";
                case '\t':
                    return "\\t";
                case '\v':
                    return "\\v";
                default:
                    return character.ToString();
            }
        }

        public static string Escape(this string characters)
        {
            return string.Join(string.Empty, characters.Select(c => c.Escape()));
        }
    }
}
