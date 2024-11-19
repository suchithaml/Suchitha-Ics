using System;
using System.Security.Policy;

class Program
{
    const int TotalFare = 500;

    static void Main()
    {
        Console.WriteLine("Enter Name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter Age:");
        int age = int.Parse(Console.ReadLine());

        if (age <= 5)
        {
            Console.WriteLine($"{name} gets a Free Ticket (Little Champs).");
        }
        else if (age >= 60)
        {
            Console.WriteLine($"{name} gets a Senior Citizen discount. Total fare: {TotalFare - (TotalFare * 30 / 100)}");
        }
        else
        {
            Console.WriteLine($"{name} has to pay the full fare: {TotalFare}");
        }
    }
}
