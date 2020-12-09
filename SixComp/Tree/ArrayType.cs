﻿namespace SixComp
{
    public partial class Tree
    {
        public class ArrayType : AnyType
        {
            private ArrayType(AnyType elementType)
            {
                ElementType = elementType;
            }

            public AnyType ElementType { get; }

            public static ArrayType From(AnyType elementType)
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