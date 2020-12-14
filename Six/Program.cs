using System;
using System.IO;

namespace Six
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var program = new Program();

            program.Init();

            program.Run();

            _ = Console.ReadKey();
        }

        public void Init()
        {
            Console.WriteLine("Six -- 0.0.1");

            var tools = new Externals();

            var gen = new GenVM();

            gen.Ops();


            var vm_dll = Path.Combine(".", "VM.asm -s VM.fas");

            tools.Fasm(vm_dll);
        }

        public void Run()
        {
            var x = 10;
            var y = 12;

            VM.WriteMessage("==>");
            Console.WriteLine($"{x} + {y} == {VM.Add(x, y)}");

            var builder = VM.CreateBuilder();
            var ptr = builder;


            ptr = VM.EmitI8(ptr, 10);

            ptr = VM.EmitI8(ptr, 12);

            ptr = VM.EmitAdd(ptr);

            _ = VM.EmitRet(ptr);

            var result = VM.Execute(builder);
            Console.WriteLine($"result: {result}");

            VM.DestroyBuilder(builder);

            var sum = VM.GetStackAt(0);
            Console.WriteLine($"sum: {sum}");
        }
    }
}
