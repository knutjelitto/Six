using Six.Support;
using SixPeg.Matches;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public interface IMatcher : IWritable
    {
        IMatcher Space { get; set; }
        bool Match(Context subject, ref int cursor);
        IEnumerable<IMatch> Matches(Context subject, int cursor);

        /// <summary>
        /// true, if this matches a single character
        /// </summary>
        bool IsClassy { get; set; }
        //bool IsTerminal { get; set; }
        string DDLong { get; }
        string Marker { get; }
    }
}
