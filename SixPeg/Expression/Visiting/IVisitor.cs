namespace SixPeg.Expression
{
    public interface IVisitor<T>
    {
        T Visit(AndExpression expr);
        T Visit(CharacterClassExpression expr);
        T Visit(CharacterRangeExpression expr);
        T Visit(CharacterSequenceExpression expr);
        T Visit(ChoiceExpression expr);
        T Visit(ErrorExpression expr);
        T Visit(Grammar expr);
        T Visit(NotExpression expr);
        T Visit(QuantifiedExpression expr);
        T Visit(ReferenceExpression expr);
        T Visit(RuleExpression expr);
        T Visit(TerminalExpression expr);
        T Visit(SequenceExpression expr);
        T Visit(SpacedExpression expr);
        T Visit(WildcardExpression expr);
        T Visit(BeforeExpression expr);
    }
}
