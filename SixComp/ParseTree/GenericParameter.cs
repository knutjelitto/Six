namespace SixComp.ParseTree
{
    public class GenericParameter
    {
        public GenericParameter(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static GenericParameter Parse(Parser parser)
        {
            var name = Name.Parse(parser);

            return new GenericParameter(name);
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
