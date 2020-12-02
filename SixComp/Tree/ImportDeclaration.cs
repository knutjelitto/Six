using SixComp.Support;

namespace SixComp.Tree
{
    public class ImportDeclaration : AnyDeclaration
    {
        public ImportDeclaration(ImportKind? kind, ImportPath path)
        {
            Kind = kind;
            Path = path;
        }

        public ImportKind? Kind { get; }
        public ImportPath Path { get; }

        public static ImportDeclaration Parse(Parser parser)
        {
            // already parsed //parser.Consume(ToKind.KwImport);

            var kind = (ImportKind?)null;
            switch (parser.Current)
            {
                case ToKind.KwTypealias:
                case ToKind.KwStruct:
                case ToKind.KwClass:
                case ToKind.KwEnum:
                case ToKind.KwProtocol:
                case ToKind.KwLet:
                case ToKind.KwVar:
                case ToKind.KwFunc:
                    kind = ImportKind.Parse(parser);
                    break;
            }
            var path = ImportPath.Parse(parser);

            return new ImportDeclaration(kind, path);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"import {Kind}{Path}");
        }

        public override string ToString()
        {
            return $"import {Kind}{Path}";
        }
    }
}
