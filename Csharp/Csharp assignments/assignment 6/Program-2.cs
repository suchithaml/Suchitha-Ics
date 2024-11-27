using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter words separated by spaces:");
        string[] words = Console.ReadLine().Split(' ');

        foreach (string word in words)
        {
            if (word.StartsWith("a") && word.EndsWith("m"))
            {
                Console.WriteLine(word);
            }
        }
        Console.ReadLine( );
    }
}