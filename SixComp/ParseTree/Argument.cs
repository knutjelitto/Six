namespace SixComp.ParseTree
{
    public class Argument
    {
        public Argument(Name? name, Expression value)
        {
            Name = name;
            Value = value;
        }

        public Name? Name { get; }
        public Expression Value { get; }
    }
}
