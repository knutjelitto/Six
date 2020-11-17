using SixComp.Support;

namespace SixComp.ParseTree
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
            parser.Consume(ToKind.KwImport);

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
            var kind = Kind == null ? string.Empty : $"{Kind} ";

            writer.WriteLine($"import {Kind}{Path}");
        }
    }
}
