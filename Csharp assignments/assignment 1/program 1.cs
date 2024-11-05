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
            Console.Write("Enter the first integer: ");
            int firstNumber = Convert.ToInt32(Console.ReadLine());

           
            Console.Write("Enter the second integer: ");
            int secondNumber = Convert.ToInt32(Console.ReadLine());

            
            if (firstNumber == secondNumber)
            {
                Console.WriteLine("The two integers are equal.");
            }
            else
            {
                Console.WriteLine("The two integers are not equal.");
            }
            Console.ReadLine();
        }
    }
}
