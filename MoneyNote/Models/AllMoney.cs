using System;

namespace MoneyNote.Models
{
    public class AllMoney
    {
        public int Id { get; set; }
        public decimal MyCahsMoney { get; set; }
        public decimal MyCardMoney { get; set; }
        public decimal AllTimeOutlay { get; set; }
        public decimal AllTimeSavings { get; set; }
    }
    public class Save
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
