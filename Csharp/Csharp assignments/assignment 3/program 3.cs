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
        public char TransactionType { get; set; }  // D for Deposit, W for Withdrawal
        public int Amount { get; set; }
        public decimal Balance { get; set; }

        // Constructor to initialize Account details
        public Accounts(string accountNo, string customerName, string accountType, decimal initialBalance)
        {
            AccountNo = accountNo;
            CustomerName = customerName;
            AccountType = accountType;
            Balance = initialBalance; // Initial Balance
        }

        // Function to handle Deposit (Credit)
        public void Credit(int amount)
        {
            Balance += amount; // Increase balance by amount deposited
            Console.WriteLine("Deposited: " + amount);
        }

        // Function to handle Withdrawal (Debit)
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

        // Function to update balance based on transaction type
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

        // Function to show account details
        public void ShowData()
        {
            Console.WriteLine("Account No: " + AccountNo);
            Console.WriteLine("Customer Name: " + CustomerName);
            Console.WriteLine("Account Type: " + AccountType);
            Console.WriteLine("Current Balance: " + Balance);
        }

        // Main method where program execution starts (only one Main method in the program)
        static void Main(string[] args)
        {
            // Create an account with initial details
            Accounts account1 = new Accounts("A12345", "John Doe", "Savings", 1000.00m);

            // Display initial account details
            Console.WriteLine("Initial Account Details:");
            account1.ShowData();
            Console.WriteLine();

            // Accept transaction details
            Console.Write("Enter transaction type (D for Deposit, W for Withdrawal): ");
            char transactionType = Char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            Console.Write("Enter the amount: ");
            int amount = int.Parse(Console.ReadLine());

            // Set transaction type and amount
            account1.TransactionType = transactionType;
            account1.Amount = amount;

            // Update balance based on transaction type
            account1.UpdateBalance();

            // Display updated account details
            Console.WriteLine("\nUpdated Account Details:");
            account1.ShowData();

            // Wait for user input before closing the console window
            Console.ReadLine();
        }
    }
}
