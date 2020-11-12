namespace SixComp.ParseTree
{
    public class TypeName : IType
    {
        public TypeName(Name name, TypeList? arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Name Name { get; }
     
        public TypeList? Arguments { get; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
