using System;
namespace MoneyNote.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public CategoryDto Category { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public char MathSymbol { get; set; }
        public bool Utility { get; set; }
    }
    public enum TransactionType
    {
        None,
        Save,
        Spend,
        Add,
        Bank
    }
}
