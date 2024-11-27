using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Accounts
    {
        
            public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string AccountType { get; set; }
        public char TransactionType { get; set; }  
        public int Amount { get; set; }
        public decimal Balance { get; set; }

        public Accounts(string accountNo, string customerName, string accountType, decimal initialBalance)
        {
            AccountNo = accountNo;
            CustomerName = customerName;
            AccountType = accountType;
            Balance = initialBalance; 
        }

        public void Credit(int amount)
        {
            Balance += amount; 
            Console.WriteLine("Deposited: " + amount);
        }

        public void Debit(int amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount; // Decrease balance by amount withdrawn
                Console.WriteLine("Withdrawn: " + amount);
            }
            else
            {
                Console.WriteLine("Insufficient balance for withdrawal!");
            }
        }

        public void UpdateBalance()
        {
            if (TransactionType == 'D' || TransactionType == 'd') // Deposit
            {
                Credit(Amount);
            }
            else if (TransactionType == 'W' || TransactionType == 'w') // Withdrawal
            {
                Debit(Amount);
            }
            else
            {
                Console.WriteLine("Invalid Transaction Type!");
            }
        }

        public void ShowData()
        {
            Console.WriteLine("Account No: " + AccountNo);
            Console.WriteLine("Customer Name: " + CustomerName);
            Console.WriteLine("Account Type: " + AccountType);
            Console.WriteLine("Current Balance: " + Balance);
        }

        static void Main(string[] args)
        {
            Accounts account1 = new Accounts("A12345", "John Doe", "Savings", 1000.00m);

            Console.WriteLine("Initial Account Details:");
            account1.ShowData();
            Console.WriteLine();

            Console.Write("Enter transaction type (D for Deposit, W for Withdrawal): ");
            char transactionType = Char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            Console.Write("Enter the amount: ");
            int amount = int.Parse(Console.ReadLine());

            account1.TransactionType = transactionType;
            account1.Amount = amount;

            account1.UpdateBalance();

            Console.WriteLine("\nUpdated Account Details:");
            account1.ShowData();

            Console.ReadLine();
        }
    }
}
