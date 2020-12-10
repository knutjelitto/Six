namespace SixComp
{
    public partial class ParseTree
    {
        public class ArrayType : IType
        {
            private ArrayType(IType elementType)
            {
                ElementType = elementType;
            }

            public IType ElementType { get; }

            public static ArrayType From(IType elementType)
            {
                return new ArrayType(elementType);
            }

            public override string ToString()
            {
                return $"[{ElementType}]";
            }
        }
    }
}