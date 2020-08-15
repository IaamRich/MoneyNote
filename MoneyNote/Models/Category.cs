using ReactiveUI;

namespace MoneyNote.Models
{
    public class Category : ReactiveObject // Reactive Object exists here to realize Fody.PropertyChanged to property "IsSelected"
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public TransactionType Type { get; set; }
        public bool IsSelected { get; set; }
    }
    public enum TransactionType
    {
        Market,
        Bar,
        Transport,
        Business,
        Network,
        Entertainment,
        Gift,
        Salary,
        Earnings,
        Other
    }
}
