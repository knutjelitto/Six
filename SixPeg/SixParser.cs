using Six.Support;
using SixPeg.Expression;

namespace SixPeg
{
    public partial class SixParser
    {
        public GrammarPart Parse(Source source)
        {
            return Parse(source.Text, source.Name);
        }
    }
}
