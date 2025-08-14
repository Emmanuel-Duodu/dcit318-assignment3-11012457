using System;
using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.Accounts
{
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine($"Transaction declined - Insufficient funds (Available: {Balance:C}, Required: {transaction.Amount:C})");
            }
            else
            {
                decimal previousBalance = Balance;
                Balance -= transaction.Amount;
                Console.WriteLine($"Transaction approved - Balance updated from {previousBalance:C} to {Balance:C}");
            }
        }
    }
}
