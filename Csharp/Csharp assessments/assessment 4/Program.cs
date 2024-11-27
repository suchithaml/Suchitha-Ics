using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace EmployeeListApp
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> empList = new List<Employee>
            {
                new Employee { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla", Title = "Manager", DOB = new DateTime(1984, 11, 16), DOJ = new DateTime(2011, 06, 08), City = "Mumbai" },
                new Employee { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla", Title = "AsstManager", DOB = new DateTime(1994, 08, 20), DOJ = new DateTime(2012, 07, 07), City = "Mumbai" },
                new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza", Title = "Consultant", DOB = new DateTime(1987, 11, 14), DOJ = new DateTime(2015, 04, 12), City = "Pune" },
                new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE", DOB = new DateTime(1990, 06, 03), DOJ = new DateTime(2016, 02, 02), City = "Pune" },
                new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE", DOB = new DateTime(1991, 03, 08), DOJ = new DateTime(2016, 02, 02), City = "Mumbai" },
                new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak", Title = "Consultant", DOB = new DateTime(1989, 11, 07), DOJ = new DateTime(2014, 08, 08), City = "Chennai" },
                new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant", DOB = new DateTime(1989, 12, 02), DOJ = new DateTime(2015, 06, 01), City = "Mumbai" },
                new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey", Title = "Associate", DOB = new DateTime(1993, 11, 11), DOJ = new DateTime(2014, 11, 06), City = "Chennai" },
                new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = new DateTime(1992, 08, 12), DOJ = new DateTime(2014, 12, 03), City = "Chennai" },
                new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = new DateTime(1991, 04, 12), DOJ = new DateTime(2016, 01, 02), City = "Pune" }
            };

            Console.WriteLine("a. Details of all employees:");
            var allEmployees = empList.Select(emp => emp);
            foreach (var emp in allEmployees)
            {
                Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.FirstName} {emp.LastName}, Title: {emp.Title}, City: {emp.City}");
            }

            Console.WriteLine("\nb. Employees whose location is not Mumbai:");
            var notMumbaiEmployees = empList.Where(emp => emp.City != "Mumbai");
            foreach (var emp in notMumbaiEmployees)
            {
                Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.FirstName} {emp.LastName}, City: {emp.City}");
            }

            Console.WriteLine("\nc. Employees with title 'AsstManager':");
            var asstManagers = empList.Where(emp => emp.Title == "AsstManager");
            foreach (var emp in asstManagers)
            {
                Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.FirstName} {emp.LastName}, Title: {emp.Title}");
            }

            Console.WriteLine("\nd. Employees whose last name starts with 'S':");
            var lastNameStartsWithS = empList.Where(emp => emp.LastName.StartsWith("S"));
            foreach (var emp in lastNameStartsWithS)
            {
                Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.FirstName} {emp.LastName}, Last Name: {emp.LastName}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
