using System;
using System.IO;

namespace SixComp
{
    public class BootCore
    {
        DirectoryInfo Core = new DirectoryInfo("../../../../Six.Core");

        public BootCore()
        {
            Console.WriteLine("Boot Core");
            Console.WriteLine($"Core directory: {Core.FullName}");
        }

        public void Boot()
        {

        }
    }
}
