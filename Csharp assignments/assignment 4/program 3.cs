using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Consoleapp
{
    internal class Doctor
    {
        private int regnNo;
        private string name;
        private decimal feesCharged;
 
        public int RegnNo
        {
            get { return regnNo; }
            set { regnNo = value; }
        }
 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
 
        public decimal FeesCharged
        {
            get { return feesCharged; }
            set { feesCharged = value; }
        }
 
        public static void Main(string[] args)
        {
            Doctor doctor = new Doctor();
 
            doctor.RegnNo = 65;
            doctor.Name = "Dr.Prakruthi U M";
            doctor.FeesCharged = 500.00m;
 
            Console.WriteLine($"RegnNo: {doctor.RegnNo}");
            Console.WriteLine($"Name: {doctor.Name}");
            Console.WriteLine($"FeesCharged: {doctor.FeesCharged:C}");
            Console.ReadLine();
        }
    }
}