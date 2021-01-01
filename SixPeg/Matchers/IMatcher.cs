using Six.Support;
using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public interface IMatcher : IWritable
    {
        IMatcher Space { get; set; }
        bool Match(Context subject, ref int cursor);
        IMatch Match(Context subject, int start);
        IEnumerable<IMatch> Matches(Context subject, int cursor);

        bool IsClassy { get; }
        string DDLong { get; }
        string Marker { get; }


        T Accept<T>(IMatcherVisitor<T> visitor);
    }
}
