1. Write a C# Sharp program to accept two integers and check whether they are equal or not. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the first integer: ");
            int firstNumber = Convert.ToInt32(Console.ReadLine());

           
            Console.Write("Enter the second integer: ");
            int secondNumber = Convert.ToInt32(Console.ReadLine());

            
            if (firstNumber == secondNumber)
            {
                Console.WriteLine("The two integers are equal.");
            }
            else
            {
                Console.WriteLine("The two integers are not equal.");
            }
            Console.ReadLine();
        }
    }
}
output:
Enter the first integer: 5
Enter the second integer: 5
The two integers are equal.


2. Write a C# Sharp program to check whether a given number is positive or negative. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            double number = Convert.ToDouble(Console.ReadLine());

            
            if (number > 0)
            {
                Console.WriteLine("The number is positive.");
            }
            else if (number < 0)
            {
                Console.WriteLine("The number is negative.");
            }
            else
            {
                Console.WriteLine("The number is zero.");
            }
            Console.ReadLine();
        }
    }
}

Enter a number: -6
The number is negative.

3. Write a C# Sharp program that takes two numbers as input and performs all operations (+,-,*,/) on them and displays the result of that operation. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the first number: ");
            double num1 = Convert.ToDouble(Console.ReadLine());


            Console.Write("Enter the second number: ");
            double num2 = Convert.ToDouble(Console.ReadLine());


            double addition = num1 + num2;
            double subtraction = num1 - num2;
            double multiplication = num1 * num2;
            double division = num2 != 0 ? num1 / num2 : double.NaN;

            Console.WriteLine("\nResults:");
            Console.WriteLine($"Addition: {num1} + {num2} = {addition}");
            Console.WriteLine($"Subtraction: {num1} - {num2} = {subtraction}");
            Console.WriteLine($"Multiplication: {num1} * {num2} = {multiplication}");

            if (num2 != 0)
            {
                Console.WriteLine($"Division: {num1} / {num2} = {division}");
            }
            else
            {
                Console.WriteLine("Division: Cannot divide by zero.");
            }
            Console.ReadLine();
        }
    }
}


Enter the first number: 20
Enter the second number: 8

Results:
Addition: 20 + 8 = 28
Subtraction: 20 - 8 = 12
Multiplication: 20 * 8 = 160
Division: 20 / 8 = 2.5

4. Write a C# Sharp program that prints the multiplication table of a number as input.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number to print its multiplication table: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nMultiplication Table of {number}:");

         
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{number} x {i} = {number * i}");
            }
            Console.ReadLine();
        }
    }
}

output:
Enter a number to print its multiplication table: 5

Multiplication Table of 5:
5 x 1 = 5
5 x 2 = 10
5 x 3 = 15
5 x 4 = 20
5 x 5 = 25
5 x 6 = 30
5 x 7 = 35
5 x 8 = 40
5 x 9 = 45
5 x 10 = 50

5.  Write a C# program to compute the sum of two given integers. If two values are the same, return the triple of their sum.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static object number;
        private static object i;

        static void Main(string[] args)
        {
            Console.Write("Enter the first integer: ");
            int num1;
            bool isNum1Valid = int.TryParse(Console.ReadLine(), out num1);

            if (!isNum1Valid)
            {
                Console.WriteLine("Please enter a valid integer for the first number.");
                return;
            }

            Console.Write("Enter the second integer: ");
            int num2;
            bool isNum2Valid = int.TryParse(Console.ReadLine(), out num2);

            if (!isNum2Valid)
            {
                Console.WriteLine("Please enter a valid integer for the second number.");
                return;
            }

            
            int sum = num1 + num2;

            if (num1 == num2)
            {
                sum *= 3;             }

            Console.WriteLine($"The result is: {sum}");
            Console.ReadLine();
        }
    }
}

output:

Enter the first integer: 7
Enter the second integer: 9
The result is: 16