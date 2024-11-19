using System;
using System.Collections.Generic;

class Employee
{
    public int Id;
    public string Name;
    public string City;
    public int Salary;

    public Employee(int id, string name, string city, int salary)
    {
        Id = id;
        Name = name;
        City = city;
        Salary = salary;
    }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>();
        Console.WriteLine("Enter the number of employees:");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Enter details for employee {i + 1} (Id, Name, City, Salary):");
            string[] input = Console.ReadLine().Split(' ');
            employees.Add(new Employee(int.Parse(input[0]), input[1], input[2], int.Parse(input[3])));
        }

        Console.WriteLine("All Employees:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.Id} {emp.Name} {emp.City} {emp.Salary}");
        }

        Console.WriteLine("Employees with Salary > 45000:");
        foreach (var emp in employees)
        {
            if (emp.Salary > 45000)
            {
                Console.WriteLine($"{emp.Id} {emp.Name} {emp.City} {emp.Salary}");
            }
        }

        Console.WriteLine("Employees from Bangalore:");
        foreach (var emp in employees)
        {
            if (emp.City == "Bangalore")
            {
                Console.WriteLine($"{emp.Id} {emp.Name} {emp.City} {emp.Salary}");
            }
        }

        employees.Sort((e1, e2) => e1.Name.CompareTo(e2.Name));
        Console.WriteLine("Employees Sorted by Name:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.Id} {emp.Name} {emp.City} {emp.Salary}");
        }
        Console.ReadLine(  );
    }
}