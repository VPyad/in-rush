using System;
using System.Collections.Generic;
using InRushCore.Compilers;

namespace ConsoleAppForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            CompilersManager compilersManager = new CompilersManager();

            List<SupportedCompilers> compilers = new List<SupportedCompilers>(compilersManager.GetSupportedCompilers());

            compilers.ForEach(x => Console.WriteLine(x.Name + " " + x.Lang));
            Console.WriteLine("----------------");

            compilersManager.UpdateCompiler("Go Go", new SupportedCompilers("Go Go", "Go", "Go", "test", "test", 10000, new string[] { "1" }));

            Console.ReadLine();
        }
    }
}
