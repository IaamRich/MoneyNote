using SQLite;

namespace MoneyNote.Models
{
    public class AllMoney
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(7)]
        public decimal MyCahsMoney { get; set; }
        public decimal MyCardMoney { get; set; }
        public decimal AllTimeOutlay { get; set; }
        public decimal AllTimeSavings { get; set; }
    }
}
