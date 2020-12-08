using SixComp.Common;
using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class ImportDeclaration : Items<BaseName, Tree.ImportDeclaration>, IDeclaration
    {
        public ImportDeclaration(IScoped outer, Tree.ImportDeclaration tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Kind = tree.Kind;
        }

        public ImportKind Kind { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Import))
            {
                Kind.Report(writer, Strings.Head.Kind);
                this.ReportList(writer, Strings.Head.Path);
            }
        }

        public static IEnumerable<BaseName> Enum(IScoped outer, Tree.ImportDeclaration tree)
        {
            return tree.Path.Select(part => new BaseName(outer, part.Name));
        }
    }
}
