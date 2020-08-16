using System;
using ReactiveUI;

namespace MoneyNote.Models
{
    public class MoneyTransaction : ReactiveObject
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
    public enum TransactionType : int
    {
        None,
        Save,
        Spend,
        Add
    }
}
