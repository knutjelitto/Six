namespace SixComp.Support
{
    public static class StringExtensions
    {
        public static string StripParents(this string strip)
        {
            if (strip.StartsWith('(') && strip.EndsWith(')'))
            {
                return strip[1..^1].StripParents();
            }

            return strip;
        }

        public static string StripParents(this object strip)
        {
            return strip.ToString()!.StripParents();
        }
    }
}
