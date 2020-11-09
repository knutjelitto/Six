using System;
using System.Diagnostics;
using System.IO;

namespace Six
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            program.Init();

            program.Run();

            Console.ReadKey();
        }

        public void Init()
        {
            Console.WriteLine("Six --");

            var projects = new DirectoryInfo("../../../../..");

            Console.WriteLine($"{projects.FullName}");

            var fasmDir = Path.Combine(projects.FullName, "Tools", "fasm");
            var fasm = Path.Combine(fasmDir, "fasm.exe");
            var include = Path.Combine(fasmDir, "include");

#if false
            var tools = Path.Combine(fasmDir, "TOOLS", "WIN32");
            Exec(fasm, Path.Combine(tools, "SYMBOLS.ASM"), include);
            Exec(fasm, Path.Combine(tools, "LISTING.ASM"), include);
#endif

            var example = Path.Combine(".", "VM.asm -s VM.fas");
            Exec(fasm, example, include);
        }

        public void Tools()
        {

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

        public void Run()
        {
            var x = 10;
            var y = 12;

            VM.WriteMessage("==>");
            Console.WriteLine($"{x} + {y} == {VM.Add(x, y)}");

            var bytes = new byte[128];

            long cnt = 0;
            ref byte ptr = ref bytes[0];

            cnt += VM.EmitI8(ref ptr, 10);
            ptr = ref bytes[cnt];

            cnt += VM.EmitI8(ref ptr, 12);
            ptr = ref bytes[cnt];

            cnt += VM.EmitAdd(ref ptr);
            ptr = ref bytes[cnt];

            cnt += (int)VM.EmitRet(ref ptr);
            ptr = ref bytes[cnt];

            var result = VM.Execute(ref bytes[0]);
            Console.WriteLine($"result: {result}");

            var sum = VM.GetStackAt(0);
            Console.WriteLine($"sum: {sum}");
        }
    }
}
