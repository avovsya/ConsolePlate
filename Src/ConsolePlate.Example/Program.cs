using System;
using System.Reflection;

namespace ConsolePlate.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting ConsolePlate!");
            var plate = new Plate();
            plate.Start(args, Assembly.GetExecutingAssembly());
            Console.ReadKey();
        }
    }
}
