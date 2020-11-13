namespace SixComp.ParseTree
{
    public class TupleTypeItem
    {
        public TupleTypeItem(Name? name, IType type)
        {
            Name = name;
            Type = type;
        }

        public Name? Name { get; }
        public IType Type { get; }

        public override string ToString()
        {
            if (Name is Name name)
            {
                return $"{name}: {Type}";
            }
            return $"{Type}";
        }
    }
}
