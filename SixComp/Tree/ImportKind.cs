using SixComp.Support;

namespace SixComp.Tree
{
    public class ImportKind : IWritable
    {
        public ImportKind(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static ImportKind Parse(Parser parser)
        {
            var token = parser.ConsumeAny();

            return new ImportKind(token);
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
