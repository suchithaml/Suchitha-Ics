using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Program
    {
        static int Letterocc(string inputstr, char letter)
        {
            inputstr = inputstr.ToLower();
            letter = char.ToLower(letter);
            int count = 0;

            foreach (char c in inputstr)
            {
                if (c == letter)
                {
                    count++;
                }
            }
            return count;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("enter a string: ");
            string inputstr = Console.ReadLine();
            Console.WriteLine("enter count: ");
            char letterToCount;

            if (char.TryParse(Console.ReadLine(), out letterToCount))
            {
                int occurences=Letterocc(inputstr, letterToCount);
                Console.WriteLine($"the letter '{letterToCount}' appears {occurences} times in the string"); 
            }
            else
            {
                Console.WriteLine("enter a single character");
            }
            Console.ReadLine();
        }
    }
}
