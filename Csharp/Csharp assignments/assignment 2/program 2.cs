using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static object number;
        private static object i;

        static void Main(string[] args)
        {
            Console.Write("Enter a digit: ");
            int number = int.Parse(Console.ReadLine());

           
            Console.WriteLine("{0} {0} {0} {0}", number);  
            Console.WriteLine("{0}{0}{0}{0}", number);     
            Console.WriteLine("{0} {0} {0} {0}", number);  
            Console.WriteLine("{0}{0}{0}{0}", number);     
            Console.ReadLine();
        }
        
        }
    }

