using Six.Support;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Six.Comp
{
    public class CoreBuilder : Builder
    {
        public CoreBuilder()
            : base(new Navi())
        {
        }

        protected override List<SourceFile> Files()
        {
            var sources = Navi.Subdir(Navi.Project, "Source");

            var count = 0;
            var sourceFiles = new List<SourceFile>();
            //sourceFiles.AddRange(Enum(Navi.Subdir(sources, "Algorithms")));
            //sourceFiles.AddRange(Enum(Navi.Subdir(sources, "Numerics")));
            //sourceFiles.AddRange(Enum(Navi.Subdir(sources, "Nio")));
            //sourceFiles.AddRange(Enum(Navi.Subdir(sources, "PackageManager")));
            sourceFiles.AddRange(Enum(Navi.Subdir(sources, "Core")));
            sourceFiles.AddRange(Enum(Navi.Subdir(sources, "CoreFull")));

            //sourceFiles = sourceFiles.Skip(60).Take(10).ToList();

            return sourceFiles;

            IEnumerable<SourceFile> Enum(DirectoryInfo directory)
            {
                foreach (var coreSource in directory.EnumerateFiles("*.swift", SearchOption.AllDirectories).OrderBy(f => f.FullName[(sources.FullName.Length + 1)..]))
                {
                    count += 1;
                    var fullName = coreSource.FullName.Replace('\\', '/');
                    var shortName = fullName[(sources.FullName.Length + 1)..];

                    var skip = Path.GetFileName(shortName).StartsWith('_');

                    yield return new SourceFile(fullName, shortName, count, skip);
                }
            }
        }
    }
}
