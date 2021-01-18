using System.Linq;

namespace Six.Support
{
    public static class CharacterExtensions
    {
        public static string AsStringLiteral(this string text)
        {
            return $"\"{text.Escape()}\"";
        }

        public static string AsCharLiteral(this char text)
        {
            return $"\'{text.Escape()}\'";
        }

        public static string Escape(this char character)
        {
            switch (character)
            {
                case '\b': return "\\b";
                case '\f': return "\\f";
                case '\n': return "\\n";
                case '\r': return "\\r";
                case '\t': return "\\t";
                case '\v': return "\\v";
                case '\'': return "\\'";
                case '\"': return "\\\"";
                case '\\': return "\\\\";
                default:
                    if (character < ' ')
                    {
                        return string.Format("\\u{0:X4}", (int)character);
                    }
                    return character.ToString();
            }
        }

        public static string Escape(this string characters)
        {
            return string.Join(string.Empty, characters.Select(c => c.Escape()));
        }

        public static string Capitalize(this string text)
        {
            return char.ToUpperInvariant(text[0]) + text[1..];
        }

        public static bool IsIdentifier(this string name)
        {
            if (name.Length == 0)
            {
                return false;
            }
            if (!(char.IsLetter(name[0]) || name[0] == '_'))
            {
                return false;
            }
            for (var i = 1; i < name.Length; i += 1)
            {
                if (!(char.IsLetterOrDigit(name[i]) || name[i] == '_'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
