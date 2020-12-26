namespace SixPeg.Expression
{
    public class OptionExpression
    {
        public OptionExpression(Symbol name, object value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }
        public object Value { get; }
    }
}
