using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static object number;
        private static object i;

        static void Main(string[] args)
        {
            Console.Write("Enter the first integer: ");
            int num1;
            bool isNum1Valid = int.TryParse(Console.ReadLine(), out num1);

            if (!isNum1Valid)
            {
                Console.WriteLine("Please enter a valid integer for the first number.");
                return;
            }

            Console.Write("Enter the second integer: ");
            int num2;
            bool isNum2Valid = int.TryParse(Console.ReadLine(), out num2);

            if (!isNum2Valid)
            {
                Console.WriteLine("Please enter a valid integer for the second number.");
                return;
            }

            
            int sum = num1 + num2;

            if (num1 == num2)
            {
                sum *= 3;             }

            Console.WriteLine($"The result is: {sum}");
            Console.ReadLine();
        }
    }
}
