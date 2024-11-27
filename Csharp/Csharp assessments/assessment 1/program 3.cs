using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter the first number");
            int n1=Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter the second number");
            int n2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter the third number");
            int n3 = Convert.ToInt32(Console.ReadLine());

            int largest = n1;
            if(n2> largest)
            {
                largest = n2;
            }
            if (n3 > largest)
            {
                largest = n3;
            }
            Console.WriteLine("the largest number is: " +largest);
            Console.ReadLine();
        }
    }
}
