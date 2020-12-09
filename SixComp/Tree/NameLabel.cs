namespace SixComp
{
    public partial class Tree
    {
        public class NameLabel
        {
            public NameLabel(BaseName name)
            {
                Name = name;
            }

            public BaseName Name { get; }

            public static NameLabel Parse(Parser parser)
            {
                var name = BaseName.Parse(parser);
                parser.Consume(ToKind.Colon);

                return new NameLabel(name);
            }

            public override string ToString()
            {
                return $"{Name}: ";
            }
        }
    }
}