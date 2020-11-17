using SixComp.Support;
using System.Diagnostics;

namespace SixComp.ParseTree
{
    public class VarDeclaration : AnyDeclaration
    {
        public VarDeclaration(Name name, TypeAnnotation? type, Initializer? init)
        {
            Name = name;
            Type = type;
            Init = init;
        }

        public Name Name { get; }
        public TypeAnnotation? Type { get; }
        public Initializer? Init { get; }

        public static VarDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwVar);

            var offset = parser.Offset;

            // first try complicated property
            {
                var name = Name.Parse(parser);
                var type = parser.Try(ToKind.Colon, TypeAnnotation.Parse);
                var init = parser.Try(ToKind.Equal, Initializer.Parse);

                if (parser.Current == ToKind.LBrace)
                {
                    parser.Consume(ToKind.LBrace);

                    var prefix = Prefix.Parse(parser);

                    var getter = (PropertyGetBlock?)null;

                    switch (parser.Current)
                    {
                        case ToKind.KwGet:
                            getter
                    }

                    parser.Consume(ToKind.RBrace);
                }

                return new VarDeclaration(name, type, init);
            }

        }

        public void Write(IWriter writer)
        {
            var type = Type == null ? string.Empty : $": {Type}";
            var init = Init == null ? string.Empty : $" = {Init}";

            writer.Write($"var {Name}{type}{init}");
            writer.WriteLine();
        }
    }
}
