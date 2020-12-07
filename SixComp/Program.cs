using System;

#pragma warning disable IDE0051 // Remove unused private members

namespace SixComp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var navi = new Navi();
            new BootCore(navi, navi.SixCore).Compile("Swift");
            //new BootCore(navi, navi.SixTests).Compile("Tests");
            Console.Write("(almost) any key ... ");
            Console.ReadKey(true);
        }
    }
}
