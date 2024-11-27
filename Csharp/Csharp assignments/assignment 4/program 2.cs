using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Scholarshipp
{
    internal class Scholarship
    {
        public void merit(int marks,decimal fees)
        {
            decimal amount = 0;
            if(marks>=70 && marks<=80)
            {
                amount = fees * 0.20m;
            }
            else if(marks>80 && marks<=90)
            {
                amount = fees * 0.30m;
            }
            else if(marks>90)
            {
                amount = fees * 0.50m;
            }
            Console.WriteLine( $"The scholarship amount is:{amount:c}");
        }
        static void Main(string[] args)
        {
            Scholarship sc = new Scholarship();
            Console.WriteLine( "Enter marks:");
            int marks = int.Parse(Console.ReadLine());
            Console.WriteLine( "Enter fees:");
            decimal fees = decimal.Parse(Console.ReadLine());
            sc.merit(marks, fees);
            Console.ReadLine();
        }
    }
}