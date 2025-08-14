using System;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Interfaces;

namespace FinanceManagementSystem.Processors
{
    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Crypto Payment: Blockchain transfer for {transaction.Category} - {transaction.Amount:C}");
            Console.WriteLine($"   Hash: 0x{transaction.Id:X8} | Timestamp: {transaction.Date:MMM dd, yyyy HH:mm}");
        }
    }
}
