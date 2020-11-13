namespace SixComp.ParseTree
{
    public class TypeParser
    {
        public static IType Parse(Parser parser)
        {
            var names = new List<TypeName>();

            do
            {
                var name = parser.ParseTypeName();
                names.Add(name);
            }
            while (Match(ToKind.Dot));

            return new TypeIdentifier(names);
        }
    }
}
