// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Linq;

namespace Stage0
{
    partial class Program
    {
        static void Main(String[] args)
        {
            Welcome5840();
            Wellcome9047();
            Console.ReadLine();
        }

        private static void Welcome5840()
        {
            System.Console.WriteLine("Enter Your Name: ");
            string? name = Console.ReadLine();
            System.Console.WriteLine(name + ", welcome to my first console application");
        }
        static partial void Wellcome9047();
    }
}
