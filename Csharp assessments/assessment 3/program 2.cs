using System;
using System.Security.Policy;

namespace BoxAdd
{
    public class Box
    {
        public double Length { get; set; }
        public double Breadth { get; set; }

        public Box(double length = 0, double breadth = 0)
        {
            Length = length;
            Breadth = breadth;
        }

        public static Box operator +(Box b1, Box b2)
        {
            return new Box(b1.Length + b2.Length, b1.Breadth + b2.Breadth);
        }
        public void Display()
        {
            Console.WriteLine($"Length: {Length}, Breadth: {Breadth}");
        }
    }
    class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the dimensions of box1:");
            Console.Write("length: ");
            double l1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("breadth: ");
            double br1 = Convert.ToDouble(Console.ReadLine());
            Box box1 = new Box(l1, br1);

            Console.WriteLine("Enter the dimensions of box2:");
            Console.Write("length: ");
            double l2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("breadth: ");
            double br2 = Convert.ToDouble(Console.ReadLine());
            Box box2 = new Box(l2, br2);

            Box box3 = box1 + box2;

            Console.WriteLine("Details of the third box (after addition):");
            box3.Display();

            Console.ReadLine();
        }
    }
}