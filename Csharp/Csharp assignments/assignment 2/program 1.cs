using System;


namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Enter the first number: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter the second number: ");
            int num2 = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nBefore Swapping: num1 = {num1}, num2 = {num2}");

            
            int temp = num1;
            num1 = num2;
            num2 = temp;

            Console.WriteLine($"After Swapping: num1 = {num1}, num2 = {num2}");
            Console.ReadLine();
        }
    }
}
