using SixComp.Support;
using System.Diagnostics;

namespace SixComp
{
    public partial class Tree
    {
        public class ClosureExpression : BaseExpression, AnyPrimaryExpression
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.LBrace);

            public ClosureExpression(bool minimal, CaptureClause captures, ClosureParameterClause parameters, bool throws, FunctionResult? result, StatementList statements)
            {
                Minimal = minimal;
                Captures = captures;
                Parameters = parameters;
                Throws = throws;
                Result = result;
                Statements = statements;
            }

            public override string ToString()
            {
                var throws = Throws ? " throws" : string.Empty;
                return $"{{ {Captures}{Parameters}{throws}{Result} {Statements} }}";
            }

            public bool Minimal { get; }
            public CaptureClause Captures { get; }
            public ClosureParameterClause Parameters { get; }
            public bool Throws { get; }
            public FunctionResult? Result { get; }
            public StatementList Statements { get; }

            public bool BlockOnly => Minimal;

            public static ClosureExpression? TryParse(Parser parser)
            {
                var outerOffset = parser.Offset;

                parser.Consume(ToKind.LBrace);

                var innerOffset = parser.Offset;
                var minimal = false;

                var captures = parser.TryList(CaptureClause.Firsts, CaptureClause.Parse);
                var parameters = ClosureParameterClause.TryParse(parser) ?? new ClosureParameterClause();
                var throws = parser.Match(ToKind.KwThrows);
                var result = parser.Try(FunctionResult.Firsts, FunctionResult.Parse);
                var inNeeded = parameters.Definite || throws || result != null;
                if (inNeeded || parser.Current == ToKind.KwIn)
                {
                    if (parser.Current != ToKind.KwIn)
                    {
                        Debug.Assert(true);
                    }
                    parser.Consume(ToKind.KwIn);
                }
                else
                {
                    parameters = new ClosureParameterClause();
                    minimal = true;
                    parser.Offset = innerOffset;
                }
                var statements = StatementList.Parse(parser, new TokenSet(ToKind.RBrace));

                if (minimal && statements.Missing && parser.Current != ToKind.RBrace)
                {
                    parser.Offset = outerOffset;
                    return null;
                }

                parser.Consume(ToKind.RBrace);

                return new ClosureExpression(minimal, captures, parameters, throws, result, statements);
            }
        }
    }
}
