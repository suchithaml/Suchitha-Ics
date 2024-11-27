using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static object number;
        private static object i;

        static void Main(string[] args)
        {
            Console.Write("Enter a day number (1 for Monday, 2 for Tuesday, ..., 7 for Sunday): ");
            int dayNumber = int.Parse(Console.ReadLine());

           
            string dayName;

            switch (dayNumber)
            {
                case 1:
                    dayName = "Monday";
                    break;
                case 2:
                    dayName = "Tuesday";
                    break;
                case 3:
                    dayName = "Wednesday";
                    break;
                case 4:
                    dayName = "Thursday";
                    break;
                case 5:
                    dayName = "Friday";
                    break;
                case 6:
                    dayName = "Saturday";
                    break;
                case 7:
                    dayName = "Sunday";
                    break;
                default:
                    dayName = "Invalid day number";
                    break;
            }

           
            Console.WriteLine($"The day is: {dayName}");
            Console.ReadLine();
        }
        
        }
    }

