using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter the string:");
            string inputstr = Console.ReadLine();

            Console.WriteLine("enter the position to remove the desired index");
            int position=int.Parse(Console.ReadLine());

            if (position >= 0 && position < inputstr.Length)
            {
                string resultstr = RemoveCharacter(inputstr, position);
                Console.WriteLine("string after removing the character at position {0}: {1}", position, resultstr);
            }
            else 
            {
                Console.WriteLine("position not valid.");
            }
            Console.ReadKey();
        }
        static string RemoveCharacter(string str, int position)
        {
            return str.Remove(position, 1);
        }
    }
}
