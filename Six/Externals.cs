using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Six
{
    public class Externals
    {
        private readonly DirectoryInfo projects;
        private readonly string fasmDir;
        private readonly string fasmExe;
        private readonly string includeDir;
        private readonly string toolsDir;

        public Externals()
        {
            projects = new DirectoryInfo("../../../../..");
            fasmDir = Path.Combine(projects.FullName, "Tools", "fasm");
            fasmExe = Path.Combine(fasmDir, "fasm.exe");
            includeDir = Path.Combine(fasmDir, "include");
            toolsDir = Path.Combine(fasmDir, "TOOLS", "WIN32");

#if false
            Exec(fasmExe, Path.Combine(toolsDir, "SYMBOLS.ASM symbols.exe"), includeDir);
            Exec(fasmExe, Path.Combine(toolsDir, "LISTING.ASM listing.exe"), includeDir);
#endif
        }

        public void Fasm(string arguments)
        {
            Exec(fasmExe, arguments, includeDir);
        }

        public void Exec(string program, string arguments, string include)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    FileName = program,
                    Arguments = arguments,
                }
            };

            process.StartInfo.Environment["INCLUDE"] = include;

            var watch = new Stopwatch();
            watch.Start();
            process.Start();
            process.WaitForExit();
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed}");
        }
    }
}
