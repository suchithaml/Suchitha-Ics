using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a word: ");
            string word = Console.ReadLine();

            
            int length = word.Length;
            Console.WriteLine("The length of the word is: " + length);
            Console.ReadLine();
        }
    }
}
