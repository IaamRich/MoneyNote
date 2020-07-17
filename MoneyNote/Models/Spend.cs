using System;
using SQLite;

namespace MoneyNote.Models
{
    public class Spend
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(7)]
        public decimal Amount { get; set; }
        [MaxLength(50)]
        public string WhereText { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
