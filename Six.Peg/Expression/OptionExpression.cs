namespace SixPeg.Expression
{
    public class OptionExpression
    {
        public OptionExpression(Symbol name, Symbol value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }
        public Symbol Value { get; }
    }
}
