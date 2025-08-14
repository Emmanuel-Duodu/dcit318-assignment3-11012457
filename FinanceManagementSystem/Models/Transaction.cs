using System;

namespace FinanceManagementSystem.Models
{
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category)
    {
        public string GetTransactionSummary() => $"Transaction #{Id}: {Category} - {Amount:C} on {Date:yyyy-MM-dd}";
    }
}
