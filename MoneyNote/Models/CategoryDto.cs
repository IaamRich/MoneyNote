namespace MoneyNote.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public CategoryType Type { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public CategoryType Type { get; set; }
        public bool IsSelected { get; set; }
    }

    public enum CategoryType
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
