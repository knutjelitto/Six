using Six.Support;

namespace SixPeg.Matchers
{
    public interface IMatcher : IWritable
    {
        IMatcher Space { get; set; }
        bool Match(Context subject, ref int cursor);

        /// <summary>
        /// true, if this matches a single character
        /// </summary>
        bool IsClassy { get; }
        bool IsPredicate { get; }

        string DDShort { get; }
        string DDLong { get; }
    }
}
