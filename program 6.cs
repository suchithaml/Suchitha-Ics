using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {


        static void Main(string[] args)
        {
            int[] sourceArray = { 1, 2, 3, 4, 5 };

            int[] targetArray = new int[sourceArray.Length];

            for (int i = 0; i < sourceArray.Length; i++)
            {
                targetArray[i] = sourceArray[i];
            }

            Console.WriteLine("Elements in the target array:");
            for (int i = 0; i < targetArray.Length; i++)
            {
                Console.Write(targetArray[i] + " ");
            }
            Console.ReadLine() ;

        }
    }
}

