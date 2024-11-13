using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static string SwapFirstLast(string str)
    {
        if (str.Length <= 1)
        {
            return str;
        }
        return str[str.Length - 1] + str.Substring(1, str.Length - 2) + str[0];
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("enter the string: ");
        string userInput = Console.ReadLine();

        string result = SwapFirstLast(userInput);
        Console.WriteLine("result: " + result);
        Console.ReadLine();
    }

}