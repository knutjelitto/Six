namespace SixComp.Support
{
    public static class StringExtensions
    {
        public static string StripParents(this string strip)
        {
#if false
            if (strip.StartsWith('(') && strip.EndsWith(')'))
            {
                return strip[1..^1].StripParents();
            }
#endif

            return strip;
        }

        public static string StripParents(this object strip)
        {
            return strip.ToString()!.StripParents();
        }

        public static string Str(this object? any)
        {
            return any?.ToString() ?? string.Empty;
        }

        public static string SpEnd(this object? any)
        {
            return any == null ? string.Empty : $"{any} ";
        }

        public static string Iff(this bool iff, string then)
        {
            return iff ? $"{then} " : string.Empty;
        }
    }
}
