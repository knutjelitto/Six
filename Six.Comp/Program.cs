using System;

namespace Six.Comp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine($"{arg}");
            }

            BuildCore();

            Console.Write("done ... ");
            _ = Console.ReadKey(true);
        }

        private static void BuildCore()
        {
            var builder = new CoreBuilder();
            var ok = builder.Build();
            if (!ok)
            {
                Console.WriteLine("FAIL");
            }
        }
    }
}
