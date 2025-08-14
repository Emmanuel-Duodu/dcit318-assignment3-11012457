using System;
using System.Collections.Generic;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Processors;
using FinanceManagementSystem.Accounts;
using FinanceManagementSystem.Interfaces;

namespace FinanceManagementSystem
{
    public class FinanceApp
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            Console.WriteLine("=== Personal Finance Management System ===\n");
            
            SavingsAccount account = new SavingsAccount("ACC-2024-001", 1500);
            Console.WriteLine($"Account created: {account.AccountNumber} with initial balance: {account.Balance:C}\n");

            Transaction t1 = new Transaction(101, DateTime.Now.AddDays(-2), 250, "Food & Dining");
            Transaction t2 = new Transaction(102, DateTime.Now.AddDays(-1), 120, "Electricity Bill");
            Transaction t3 = new Transaction(103, DateTime.Now, 300, "Movies & Games");

            var processors = new Dictionary<string, ITransactionProcessor>
            {
                ["mobile"] = new MobileMoneyProcessor(),
                ["bank"] = new BankTransferProcessor(),
                ["crypto"] = new CryptoWalletProcessor()
            };

            ProcessTransaction(processors["mobile"], t1, account);
            ProcessTransaction(processors["bank"], t2, account);
            ProcessTransaction(processors["crypto"], t3, account);

            DisplayTransactionHistory();
        }

        private void ProcessTransaction(ITransactionProcessor processor, Transaction transaction, SavingsAccount account)
        {
            processor.Process(transaction);
            account.ApplyTransaction(transaction);
            _transactions.Add(transaction);
            Console.WriteLine();
        }

        private void DisplayTransactionHistory()
        {
            Console.WriteLine("=== Transaction History ===");
            foreach (var transaction in _transactions)
            {
                Console.WriteLine(transaction.GetTransactionSummary());
            }
        }

        public static void Main()
        {
            FinanceApp app = new FinanceApp();
            app.Run();
        }
    }
}
