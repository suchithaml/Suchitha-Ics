using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Enter the file name (with extension): ");
        string filePath = Console.ReadLine(); 

        Console.Write("Enter the text you wish to add to the file: ");
        string textToAppend = Console.ReadLine(); 

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(textToAppend);
            }

            Console.WriteLine($"Text successfully added to the file \"{filePath}\".");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        Console.ReadLine();
    }
}