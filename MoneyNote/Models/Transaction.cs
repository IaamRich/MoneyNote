using System;
using ReactiveUI;

namespace MoneyNote.Models
{
    public class Transaction : ReactiveObject
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public CategoryType CategoryType { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
    public enum TransactionType
    {
        Save,
        Spend,
        Add
    }
}
