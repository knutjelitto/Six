using System;

namespace SixPeg.Matches
{
    public class Match : IMatch
    {
        public static readonly IMatch None = new Match();

        public static IMatch Success(int space, int start, int next)
        {
            throw new NotImplementedException();
        }

        public static IMatch Fail(int start)
        {
            throw new NotImplementedException();
        }
    }
}
