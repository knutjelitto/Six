using SixComp.Support;
using System.IO;

namespace SixComp
{
    public class Dumper
    {
        public static void ParsedSwift(Context context, ParseTree.CompilationUnit unit)
        {
            var treeName = Path.ChangeExtension(context.File.Name, ".parsed.swift");
            var treeFile = Path.Combine(context.Temp.FullName, treeName);
            Directory.CreateDirectory(Path.GetDirectoryName(treeFile)!);
            using (var writer = new FileWriter(treeFile))
            {
                unit.Write(writer);
            }
        }
    }
}
