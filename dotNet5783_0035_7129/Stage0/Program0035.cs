// See https://aka.ms/new-console-template for more information
using System;
namespace stage0
{
     partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0035();
            Welcome7129();
            Console.ReadKey();
        }
        static partial void Welcome7129();
        private static void Welcome0035()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write(name);
            Console.Write(", welcome to my first consle application");
        }
    }
}




