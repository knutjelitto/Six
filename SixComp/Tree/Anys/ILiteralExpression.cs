using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface ILiteralExpression : IPrimaryExpression
        {
            Token Token { get; }

            public abstract class AnyLiteralExpression : BaseExpression, ILiteralExpression
            {
                public AnyLiteralExpression(Token token)
                {
                    Token = token;
                }

                public Token Token { get; }

                public override string ToString()
                {
                    return $"{Token}";
                }
            }

            public class BoolLiteralExpression : AnyLiteralExpression
            {
                public static readonly TokenSet Firsts = new TokenSet(ToKind.False, ToKind.True);

                private BoolLiteralExpression(Token token) : base(token)
                {
                }

                public static BoolLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(Firsts);

                    return new BoolLiteralExpression(token);
                }
            }

            public class ColumnLiteralExpression : AnyLiteralExpression
            {
                private ColumnLiteralExpression(Token token)
                    : base(token)
                {
                }

                public static ColumnLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.CdColumn);

                    return new ColumnLiteralExpression(token);
                }
            }

            public class FileLiteralExpression : AnyLiteralExpression
            {
                private FileLiteralExpression(Token token)
                    : base(token)
                {
                }

                public static FileLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.CdFile);

                    return new FileLiteralExpression(token);
                }
            }

            public class FunctionLiteralExpression : AnyLiteralExpression
            {
                private FunctionLiteralExpression(Token token)
                    : base(token)
                {
                }

                public static FunctionLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.CdFunction);

                    return new FunctionLiteralExpression(token);
                }
            }

            public class LineLiteralExpression : AnyLiteralExpression
            {
                private LineLiteralExpression(Token token)
                    : base(token)
                {
                }

                public static LineLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.CdLine);

                    return new LineLiteralExpression(token);
                }
            }

            public class NilLiteralExpression : AnyLiteralExpression
            {
                public NilLiteralExpression(Token token) : base(token) { }

                public static NilLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.KwNil);

                    return new NilLiteralExpression(token);
                }
            }

            public sealed class NumberLiteralExpression : AnyLiteralExpression
            {
                public NumberLiteralExpression(Token token) : base(token)
                {
                }

                public static NumberLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.Number);

                    return new NumberLiteralExpression(token);
                }
            }

            public class StringLiteralExpression : AnyLiteralExpression
            {
                public StringLiteralExpression(Token token) : base(token)
                {
                }

                public static StringLiteralExpression Parse(Parser parser)
                {
                    var token = parser.Consume(ToKind.String);

                    return new StringLiteralExpression(token);
                }
            }
        }
    }
}