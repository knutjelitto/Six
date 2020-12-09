namespace SixComp
{
    public partial class Tree
    {
        public class DictionaryType : AnyType
        {
            private DictionaryType(AnyType keyType, AnyType valueType)
            {
                KeyType = keyType;
                ValueType = valueType;
            }

            public AnyType KeyType { get; }
            public AnyType ValueType { get; }

            public static DictionaryType From(AnyType keyType, AnyType valueType)
            {
                return new DictionaryType(keyType, valueType);
            }

            public override string ToString()
            {
                return $"[{KeyType}:{ValueType}]";
            }
        }
    }
}