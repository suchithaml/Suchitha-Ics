using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NegativeNumberExceptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter an integer: ");
                int number = int.Parse(Console.ReadLine());

                CheckForNegative(number);

                Console.WriteLine("The number is non-negative.");
            }
            catch (NegativeNumberException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            Console.Read();
        }

        static void CheckForNegative(int num)
        {
            if (num < 0)
            {
                throw new NegativeNumberException("The number cannot be negative.");
            }
        }
    }

    public class NegativeNumberException : Exception
    {
        public NegativeNumberException(string message) : base(message)
        {

        }
    }
}
