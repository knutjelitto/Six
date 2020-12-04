using System.Diagnostics;

namespace SixComp.Tree
{
    public class ImportPathIdentifier
    {
        public ImportPathIdentifier(Token token)
        {
            Name = BaseName.From(token);
        }

        public BaseName Name { get; }
        public Token Token => Name.Token;

        public static ImportPathIdentifier Parse(Parser parser)
        {
            Debug.Assert(parser.Current == ToKind.Name || parser.IsOperator);

            var token = parser.ConsumeAny();

            return new ImportPathIdentifier(token);
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
