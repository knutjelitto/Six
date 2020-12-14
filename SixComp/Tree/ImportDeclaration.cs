using Six.Support;
using SixComp.Common;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ImportDeclaration : IDeclaration
        {
            public ImportDeclaration(ImportKind kind, ImportPath path)
            {
                Kind = kind;
                Path = path;
            }

            public ImportKind Kind { get; }
            public ImportPath Path { get; }

            public static ImportDeclaration Parse(Parser parser)
            {
                // already parsed //parser.Consume(ToKind.KwImport);

                var kind = ImportKind.AnyAndAll;
                var consume = true;
                switch (parser.Current)
                {
                    case ToKind.KwTypealias:
                        kind = ImportKind.Typealias;
                        break;
                    case ToKind.KwStruct:
                        kind = ImportKind.Struct;
                        break;
                    case ToKind.KwClass:
                        kind = ImportKind.Class;
                        break;
                    case ToKind.KwEnum:
                        kind = ImportKind.Enum;
                        break;
                    case ToKind.KwProtocol:
                        kind = ImportKind.Protocol;
                        break;
                    case ToKind.KwLet:
                        kind = ImportKind.Let;
                        break;
                    case ToKind.KwVar:
                        kind = ImportKind.Var;
                        break;
                    case ToKind.KwFunc:
                        kind = ImportKind.Func;
                        break;
                    default:
                        consume = false;
                        break;
                }

                if (consume)
                {
                    parser.ConsumeAny();
                }

                var path = ImportPath.Parse(parser);

                return new ImportDeclaration(kind, path);


            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"import {Kind} {Path}");
            }

            public override string ToString()
            {
                return $"import {Kind} {Path}";
            }
        }
    }
}