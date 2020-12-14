using Six.Support;

namespace SixPeg.Matchers
{
    public interface IMatcher : IWritable
    {
        bool Match(string subject, ref int cursor);
    }
}
