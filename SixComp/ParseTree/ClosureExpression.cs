using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ClosureExpression : BaseExpression, AnyExpression
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBrace);

        public ClosureExpression(bool minimal, CaptureList captures, ClosureParameterClause? parameters, bool throws, FunctionResult? result, StatementList statements)
        {
            Minimal = minimal;
            Captures = captures;
            Parameters = parameters;
            Throws = throws;
            Result = result;
            Statements = statements;
        }

        public bool Minimal { get; }
        public CaptureList Captures { get; }
        public ClosureParameterClause? Parameters { get; }
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

            var captures = parser.TryList(CaptureList.Firsts, CaptureList.Parse);
            var parameters = parser.TryList(ClosureParameterClause.Firsts, ClosureParameterClause.Parse);
            var throws = parser.Match(ToKind.KwThrows);
            var result = parser.Try(FunctionResult.Firsts, FunctionResult.Parse);
            var inNeeded = !captures.Missing || parameters.Definite || throws || result != null;
            if (inNeeded || parser.Current == ToKind.KwIn)
            {
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
