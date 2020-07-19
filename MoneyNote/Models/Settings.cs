using SQLite;

namespace MoneyNote.Models
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CurrentLanguage { get; set; }
        public bool IsSoundOn { get; set; }
        public bool IsAdsOn { get; set; }
    }
}
