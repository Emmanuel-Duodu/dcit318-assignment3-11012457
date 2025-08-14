using System;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Interfaces;

namespace FinanceManagementSystem.Processors
{
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Mobile Payment: Processing {transaction.Category} payment of {transaction.Amount:C}");
            Console.WriteLine($"   Transaction ID: {transaction.Id} | Date: {transaction.Date:MMM dd, yyyy}");
        }
    }
}
