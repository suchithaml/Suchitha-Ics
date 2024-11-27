using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Consoleapp1
{
    using System;
 
    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public float Salary { get; set; }
 
 
        public Employee(int empId, string empName, float salary)
        {
            EmpId = empId;
            EmpName = empName;
            Salary = salary;
        }
    }
 
    class PartTimeEmployee : Employee
    {
        public float Wages { get; set; }
 
 
        public PartTimeEmployee(int empId, string empName, float salary, float wages) : base(empId, empName, salary)
        {
            Wages = wages;
        }
    }
 
    class Employe
 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter details for Full-time Employee:");
            Console.Write("Employee ID: ");
            int empId = int.Parse(Console.ReadLine());
            Console.Write("Employee Name: ");
            string empName = Console.ReadLine();
            Console.Write("Salary: ");
            float salary = float.Parse(Console.ReadLine());
 
            Employee fullTimeEmployee = new Employee(empId, empName, salary);
 
            Console.WriteLine("Enter details for Part-time Employee:");
            Console.Write("Employee ID: ");
            int partTimeEmpId = int.Parse(Console.ReadLine());
            Console.Write("Employee Name: ");
            string partTimeEmpName = Console.ReadLine();
            Console.Write("Salary: ");
            float partTimeSalary = float.Parse(Console.ReadLine());
            Console.Write("Wages: ");
            float wages = float.Parse(Console.ReadLine());
 
            PartTimeEmployee partTimeEmployee = new PartTimeEmployee(partTimeEmpId, partTimeEmpName, partTimeSalary, wages);
 
            Console.WriteLine("\nFull-time Employee Details:");
            Console.WriteLine($"Employee ID: {fullTimeEmployee.EmpId}");
            Console.WriteLine($"Employee Name: {fullTimeEmployee.EmpName}");
            Console.WriteLine($"Salary: {fullTimeEmployee.Salary}");
 
            Console.WriteLine("\nPart-time Employee Details:");
            Console.WriteLine($"Employee ID: {partTimeEmployee.EmpId}");
            Console.WriteLine($"Employee Name: {partTimeEmployee.EmpName}");
            Console.WriteLine($"Salary: {partTimeEmployee.Salary}");
            Console.WriteLine($"Wages: {partTimeEmployee.Wages}");
            Console.ReadKey();
        }
    }
}