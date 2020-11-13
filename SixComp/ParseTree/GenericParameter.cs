namespace SixComp.ParseTree
{
    public class GenericParameter
    {
        public GenericParameter(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
