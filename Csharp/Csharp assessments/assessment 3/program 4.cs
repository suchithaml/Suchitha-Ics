using System;
using System.Security.Policy;

namespace Calculator
{
    class Program
    {
        public delegate int CalculatorOperation(int a, int b);

        static void Main(string[] args)
        {

            Console.Write("Enter the 1st number: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter the 2nd number: ");
            int num2 = int.Parse(Console.ReadLine());

            PerformOperation("Addition", num1, num2, Add);
            PerformOperation("Subtraction", num1, num2, Subtract);
            PerformOperation("Multiplication", num1, num2, Multiply);
            Console.Read();

        }

        static void PerformOperation(string operationName, int num1, int num2, CalculatorOperation operation)
        {
            int result = operation(num1, num2);
            Console.WriteLine($"{operationName} of {num1} and {num2} is: {result}");
        }

        static int Add(int a, int b) => a + b;
        static int Subtract(int a, int b) => a - b;
        static int Multiply(int a, int b) => a * b;
    }
}