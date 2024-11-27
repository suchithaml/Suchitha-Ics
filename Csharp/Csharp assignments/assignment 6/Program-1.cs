using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 7, 2, 30 };
        List<string> results = GetFilteredNumbersWithSquares(numbers);

        foreach (string result in results)
        {
            Console.WriteLine(result);
            
        }
        Console.ReadLine(  );
    }

    static List<string> GetFilteredNumbersWithSquares(List<int> numbers)
    {
        List<string> output = new List<string>();
        foreach (int num in numbers)
        {
            int square = CalculateSquare(num);
            if (square > 20)
            {
                output.Add($"{num} - {square}");
            }
        }
        return output;
    }

    static int CalculateSquare(int number)
    {
        return number * number;
    }
}