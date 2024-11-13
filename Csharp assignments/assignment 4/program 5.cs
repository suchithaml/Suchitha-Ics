using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Assignment4
{
 
    public interface IStudent
    {
        int StudentId { get; set; }
        string Name { get; set; }
        void ShowDetails();
    }
 
 
    public class Dayscholar : IStudent
    {
 
        public int StudentId { get; set; }
        public string Name { get; set; }
 
 
        public Dayscholar(int studentId, string name)
        {
            StudentId = studentId;
            Name = name;
        }
 
 
        public void ShowDetails()
        {
            Console.WriteLine($"Student ID: {StudentId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine("Type: Dayscholar");
        }
    }
 
 
    public class Resident : IStudent
    {
 
        public int StudentId { get; set; }
        public string Name { get; set; }
 
 
        public Resident(int studentId, string name)
        {
            StudentId = studentId;
            Name = name;
        }
 
 
        public void ShowDetails()
        {
            Console.WriteLine($"Student ID: {StudentId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine("Type: Resident");
        }
    }
 
    class Student
 
    {
        static void Main(string[] args)
        {
 
            Console.WriteLine("Enter details for Dayscholar:");
            Console.Write("Student ID: ");
            int dayscholarId = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            string dayscholarName = Console.ReadLine();
 
 
            Dayscholar dayscholar = new Dayscholar(dayscholarId, dayscholarName);
 
            Console.WriteLine("\nEnter details for Resident:");
            Console.Write("Student ID: ");
            int residentId = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            string residentName = Console.ReadLine();
 
 
            Resident resident = new Resident(residentId, residentName);
 
 
            Console.WriteLine("\nDetails of Dayscholar:");
            dayscholar.ShowDetails();
 
            Console.WriteLine("\nDetails of Resident:");
            resident.ShowDetails();
            Console.ReadKey();
        }
    }
}
