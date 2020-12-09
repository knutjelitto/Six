using System.Collections.Generic;

namespace SixComp
{
    public partial class Tree
    {
        public class ImportPath : ItemList<ImportPathIdentifier>
        {
            public ImportPath(List<ImportPathIdentifier> list) : base(list) { }
            public ImportPath() { }

            public static ImportPath Parse(Parser parser)
            {
                var list = new List<ImportPathIdentifier>();

                do
                {
                    var identifier = ImportPathIdentifier.Parse(parser);
                    list.Add(identifier);
                }
                while (parser.Match(ToKind.Dot));

                return new ImportPath(list);
            }

            public override string ToString()
            {
                return string.Join(".", this);
            }
        }
    }
}