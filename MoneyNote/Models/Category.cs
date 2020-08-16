using ReactiveUI;

namespace MoneyNote.Models
{
    public class Category : ReactiveObject // Reactive Object exists here to realize Fody.PropertyChanged to property "IsSelected"
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public CategoryType Type { get; set; }
        public bool IsSelected { get; set; }
    }
    public enum CategoryType : int
    {
        None,
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
