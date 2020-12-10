namespace SixComp
{
    public partial class ParseTree
    {
        public class DictionaryType : IType
        {
            private DictionaryType(IType keyType, IType valueType)
            {
                KeyType = keyType;
                ValueType = valueType;
            }

            public IType KeyType { get; }
            public IType ValueType { get; }

            public static DictionaryType From(IType keyType, IType valueType)
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