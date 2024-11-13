using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BankAccount
{
    class InsufficientBalanceException:Exception
    {
        public InsufficientBalanceException(string message):base(message)
        { }
    }
    internal class BankAccount
    {
        private decimal balance;
        public BankAccount(decimal intialBalance)
        {
            balance = intialBalance;
        }
        public void deposit(decimal amount)
        {
            if(amount<=0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
 
            }
            balance += amount;
            Console.WriteLine( $"Deposited:{amount:c}.new Balance:{balance:c}.");
        }
        public void withdraw(decimal amount)
        {
            if(amount<=0)
            {
                throw new ArgumentException("withdrawal amount be positive.");
            }
            if(amount>balance)
            {
                throw new InsufficientBalanceException("Insufficient balance for the withdrawal.");
            }
            balance -= amount;
            Console.WriteLine( $"withdrew:{amount:c}.new balance:{balance:c}.");
        }
        public void checkBalance()
        {
            Console.WriteLine( $"current balance:{balance:c}.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount bank = new BankAccount(1000m);
            try
            {
                bank.deposit(200m);
                bank.withdraw(500m);
                bank.checkBalance();
                bank.withdraw(100m);
            }
            catch(InsufficientBalanceException e)
            {
                Console.WriteLine( $"Transaction error:{e.Message}");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine( $"Input error:{e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine( $"An unexpected error occured:{e.Message}");
            }
            Console.ReadLine();
        }
    }
}