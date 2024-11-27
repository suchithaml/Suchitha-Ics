using System;
using System.IO;


namespace ConsoleApp14
{
    class Program
    {
        static void Main()
        {
            try
            {
                    Console.Write("How many Lines your poetry contain:");
                    int linesCount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nEnter your Lines Below:\n");
                    string[] lines = new string[linesCount];
                    for (int i = 0; i < linesCount; i++)
                    {
                        Console.Write($"Line {i + 1}:");
                    lines[i] = Console.ReadLine();
                    }
                string filePath = "poetry.txt";

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
                Console.WriteLine("File Created and data is written to the file.");
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("You don't have an access to create a file in particular folder");
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Please enter proper data");
            }
            catch (Exception e)
            {
                Console.WriteLine("Some exception didn't handled");
            }
            Console.WriteLine("\n\nPress Enter to exit");
            Console.ReadKey();

        }
    }
}
