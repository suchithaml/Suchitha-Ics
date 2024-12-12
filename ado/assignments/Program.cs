using System;
using System.Collections.Generic;
using System.Linq;

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
    static void Main()
    {
        // Create and populate the employee list
        List<Employee> empList = new List<Employee>
        {
            new Employee { EmployeeID = 1001, FirstName = "malcolm", LastName = "daruwalla", Title = "manager", DOB = new DateTime(1984, 11, 16), DOJ = new DateTime(2011, 6, 8), City = "Mumbai" },
            new Employee { EmployeeID = 1002, FirstName = "asdin", LastName = "dhalla", Title = "asstManager", DOB = new DateTime(1984, 8, 20), DOJ = new DateTime(2012, 7, 7), City = "Mumbai" },
            new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza", Title = "Consultant", DOB = new DateTime(1987, 11, 14), DOJ = new DateTime(2015, 4, 12), City = "Pune" },
            new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE", DOB = new DateTime(1990, 6, 3), DOJ = new DateTime(2016, 2, 2), City = "Pune" },
            new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE", DOB = new DateTime(1991, 3, 8), DOJ = new DateTime(2016, 2, 2), City = "Mumbai" },
            new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak", Title = "Consultant", DOB = new DateTime(1989, 11, 7), DOJ = new DateTime(2014, 8, 8), City = "Chennai" },
            new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant", DOB = new DateTime(1989, 12, 2), DOJ = new DateTime(2015, 6, 1), City = "Mumbai" },
            new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey", Title = "Associate", DOB = new DateTime(1993, 11, 11), DOJ = new DateTime(2014, 11, 6), City = "Chennai" },
            new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = new DateTime(1992, 8, 12), DOJ = new DateTime(2014, 12, 3), City = "Chennai" },
            new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = new DateTime(1991, 4, 12), DOJ = new DateTime(2016, 1, 2), City = "Pune" }
        };

        // 1. Employees who joined before 1/1/2015
        var joinedBefore2015 = empList.Where(emp => emp.DOJ < new DateTime(2015, 1, 1));
        Console.WriteLine("Employees who joined before 1/1/2015:");
        foreach (var emp in joinedBefore2015)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, DOJ: {emp.DOJ.ToShortDateString()}");
        }

        // 2. Employees born after 1/1/1990
        var bornAfter1990 = empList.Where(emp => emp.DOB > new DateTime(1990, 1, 1));
        Console.WriteLine("\nEmployees born after 1/1/1990:");
        foreach (var emp in bornAfter1990)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, DOB: {emp.DOB.ToShortDateString()}");
        }

        // 3. Employees whose designation is Consultant or Associate
        var consultantOrAssociate = empList.Where(emp => emp.Title == "Consultant" || emp.Title == "Associate");
        Console.WriteLine("\nEmployees who are Consultants or Associates:");
        foreach (var emp in consultantOrAssociate)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Title: {emp.Title}");
        }

        // 4. Total number of employees
        Console.WriteLine($"\nTotal number of employees: {empList.Count}");

        // 5. Total number of employees belonging to Chennai
        Console.WriteLine($"\nTotal number of employees in Chennai: {empList.Count(emp => emp.City == "Chennai")}");

        // 6. Highest EmployeeID
        Console.WriteLine($"\nHighest EmployeeID: {empList.Max(emp => emp.EmployeeID)}");

        // 7. Employees who joined after 1/1/2015
        Console.WriteLine($"\nTotal employees joined after 1/1/2015: {empList.Count(emp => emp.DOJ > new DateTime(2015, 1, 1))}");

        // 8. Employees whose designation is not "Associate"
        Console.WriteLine($"\nTotal employees whose designation is not Associate: {empList.Count(emp => emp.Title != "Associate")}");

        // 9. Total number of employees based on city
        Console.WriteLine("\nEmployees grouped by city:");
        var employeesByCity = empList.GroupBy(emp => emp.City);
        foreach (var group in employeesByCity)
        {
            Console.WriteLine($"{group.Key}: {group.Count()} employees");
        }

        // 10. Total number of employees based on city and title
        Console.WriteLine("\nEmployees grouped by city and title:");
        var employeesByCityAndTitle = empList.GroupBy(emp => new { emp.City, emp.Title });
        foreach (var group in employeesByCityAndTitle)
        {
            Console.WriteLine($"{group.Key.City} - {group.Key.Title}: {group.Count()} employees");
        }

        // 11. Youngest employee
        var youngestDOB = empList.Max(emp => emp.DOB);
        var youngestEmployees = empList.Where(emp => emp.DOB == youngestDOB);
        Console.WriteLine("\nYoungest employee(s):");
        foreach (var emp in youngestEmployees)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, DOB: {emp.DOB.ToShortDateString()}");
        }
        Console.ReadLine();
    }
}
