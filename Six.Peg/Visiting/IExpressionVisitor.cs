using Six.Peg.Expression;

namespace SixPeg.Visiting
{
    public interface IExpressionVisitor<T>
    {
        T Visit(RuleExpression expr);
        T Visit(TerminalExpression expr);
        T Visit(Rule expr);

        T Visit(AndExpression expr);
        T Visit(NotExpression expr);
        T Visit(BeforeExpression expr);

        T Visit(CharacterClassExpression expr);
        T Visit(CharacterRangeExpression expr);
        T Visit(CharacterSequenceExpression expr);

        T Visit(ChoiceExpression expr);
        T Visit(ErrorExpression expr);
        T Visit(QuantifiedExpression expr);
        T Visit(ReferenceExpression expr);
        T Visit(SequenceExpression expr);
        T Visit(SpacedExpression expr);
        T Visit(WildcardExpression expr);
    }
}
