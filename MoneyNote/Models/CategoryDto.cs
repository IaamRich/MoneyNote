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
        Restaurant,
        Transport,
        Business,
        Network,
        Entertainment,
        Salary,
        Earnings,
        Gift,
        Other
    }
}
