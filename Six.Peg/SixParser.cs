using Six.Peg.Runtime;
using Six.Peg.Expression;

namespace SixPeg
{
    public partial class SixParser
    {
        protected Source Source { get; set; }
        public GrammarPart Parse(Source source)
        {
            Source = source;

            return Parse(source.Text, source.Name);
        }
    }
}
