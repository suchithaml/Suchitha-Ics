using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
       

        static void Main(string[] args)
        {
            int[] marks = new int[10];

            Console.WriteLine("Enter 10 marks:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Mark {i + 1}: ");
                marks[i] = int.Parse(Console.ReadLine());
            }

            int total = marks.Sum();

            double average = marks.Average();

            int minMarks = marks.Min();
            int maxMarks = marks.Max();

            Console.WriteLine($"\nTotal marks: {total}");
            Console.WriteLine($"Average marks: {average}");
            Console.WriteLine($"Minimum marks: {minMarks}");
            Console.WriteLine($"Maximum marks: {maxMarks}");

            Console.WriteLine("\nMarks in ascending order:");
            Array.Sort(marks);
            foreach (int mark in marks)
            {
                Console.Write(mark + " ");
            }

            Console.WriteLine("\n\nMarks in descending order:");
            Array.Reverse(marks);
            foreach (int mark in marks)
            {
                Console.Write(mark + " ");
            }
            Console.ReadLine();
        }
        
        }
    }
