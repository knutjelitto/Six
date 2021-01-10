using Six.Support;
using System.Collections.Generic;
using System.IO;

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
            var coreSources = Navi.Subdir(sources, "Core");

            return new List<SourceFile>(Enum());

            IEnumerable<SourceFile> Enum()
            {
                int count = 0;
                foreach (var coreSource in coreSources.EnumerateFiles("*.swift", SearchOption.TopDirectoryOnly))
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
