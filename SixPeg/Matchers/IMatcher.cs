using Six.Support;

namespace SixPeg.Matchers
{
    public interface IMatcher : IWritable
    {
        IMatcher Space { get; set; }
        bool Match(string subject, ref int cursor);

        string DShort { get; }
        string DLong { get; }
    }
}
