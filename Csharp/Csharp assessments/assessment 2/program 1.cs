using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Student
{
    public string Name { get; set; }
    public int StudentId { get; set; }
    public double Grade { get; set; }

    public Student(string name, int studentId, double grade)
    {
        Name = name;
        StudentId = studentId;
        Grade = grade;
    }

    public abstract bool IsPassed(double grade);
}

class Undergraduate : Student
{
    public Undergraduate(string name, int studentId, double grade) : base(name, studentId, grade) { }

    public override bool IsPassed(double grade)
    {
        return grade > 70.0;
    }
}

class Graduate : Student
{
    public Graduate(string name, int studentId, double grade) : base(name, studentId, grade) { }

    public override bool IsPassed(double grade)
    {
        return grade > 80.0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter student type (1 for Undergraduate, 2 for Graduate): ");
        int studentType = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter student name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter student ID: ");
        int studentId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter grade: ");
        double grade = double.Parse(Console.ReadLine());

        Student student;

        if (studentType == 1)
        {
            student = new Undergraduate(name, studentId, grade);
        }
        else
        {
            student = new Graduate(name, studentId, grade);
        }

        Console.WriteLine($"{student.Name} passed: {student.IsPassed(student.Grade)}");
        Console.Read();
    }
}