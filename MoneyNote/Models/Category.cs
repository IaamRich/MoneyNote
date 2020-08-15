using ReactiveUI;

namespace MoneyNote.Models
{
    public class Category : ReactiveObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public TransactionType Type { get; set; }
        public bool IsSelected { get; set; }
    }
}
