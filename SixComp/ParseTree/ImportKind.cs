using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ImportKind : IWriteable
    {
        public ImportKind(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static ImportKind Parse(Parser parser)
        {
            var token = parser.Consume();

            return new ImportKind(token);
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
