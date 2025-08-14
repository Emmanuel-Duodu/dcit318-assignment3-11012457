using System;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Interfaces;

namespace FinanceManagementSystem.Processors
{
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Bank Transfer: Executing {transaction.Category} payment of {transaction.Amount:C}");
            Console.WriteLine($"   Reference: BT-{transaction.Id:D6} | Processed: {transaction.Date:MMM dd, yyyy}");
        }
    }
}
