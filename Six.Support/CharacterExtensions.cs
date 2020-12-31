using System.Linq;

namespace Six.Support
{
    public static class CharacterExtensions
    {
        public static string Escape(this char character)
        {
            return character switch
            {
                '\b' => "\\b",
                '\f' => "\\f",
                '\n' => "\\n",
                '\r' => "\\r",
                '\t' => "\\t",
                '\v' => "\\v",
                _ => character.ToString(),
            };
        }

        public static string Escape(this string characters)
        {
            return string.Join(string.Empty, characters.Select(c => c.Escape()));
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
