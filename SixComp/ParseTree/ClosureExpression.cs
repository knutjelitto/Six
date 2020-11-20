using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ClosureExpression : AnyExpression
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBrace);

        public ClosureExpression(CaptureList? captures, ClosureParameterClause? parameters, bool throws, FunctionResult? result, StatementList statements)
        {
            Captures = captures;
            Parameters = parameters;
            Throws = throws;
            Result = result;
            Statements = statements;
        }

        public CaptureList? Captures { get; }
        public ClosureParameterClause? Parameters { get; }
        public bool Throws { get; }
        public FunctionResult? Result { get; }
        public StatementList Statements { get; }

        public static ClosureExpression? TryParse(Parser parser)
        {
            if (!parser.Match(ToKind.LBrace))
            {
                return null;
            }
            var captures = parser.Try(CaptureList.Firsts, CaptureList.Parse);
            var parameters = parser.Try(ClosureParameterClause.Firsts, ClosureParameterClause.Parse);
            bool throws = parser.Match(ToKind.KwThrows);
            var result = parser.Try(FunctionResult.Firsts, FunctionResult.Parse);
            if (!parser.Match(ToKind.KwIn))
            {
                return null;
            }
            var statements = StatementList.Parse(parser, new TokenSet(ToKind.RBrace));
            if (!parser.Match(ToKind.RBrace))
            {
                return null;
            }

            return new ClosureExpression(captures, parameters, throws, result, statements);
        }
    }
}
