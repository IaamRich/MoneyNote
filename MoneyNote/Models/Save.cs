using System;
using SQLite;

namespace MoneyNote.Models
{
    public class Save
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
