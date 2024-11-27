using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number to print its multiplication table: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nMultiplication Table of {number}:");

         
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{number} x {i} = {number * i}");
            }
            Console.ReadLine();
        }
    }
}

