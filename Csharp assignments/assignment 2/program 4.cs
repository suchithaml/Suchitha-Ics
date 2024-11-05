using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
       

        static void Main(string[] args)
        {
            int[] numbers = { 5, 10, 15, 20, 25, 30 };

            double average = numbers.Average();

            int minValue = numbers.Min();
            int maxValue = numbers.Max();

           
            Console.WriteLine($"Average value of array elements: {average}");
            Console.WriteLine($"Minimum value in the array: {minValue}");
            Console.WriteLine($"Maximum value in the array: {maxValue}");
            Console.ReadLine();
        }
        
        }
    }
