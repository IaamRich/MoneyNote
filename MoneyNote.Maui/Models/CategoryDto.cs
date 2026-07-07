namespace MoneyNote.Maui;

public class CategoryDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public CategoryType Type { get; set; }
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
    Medicine,
    Lend,
    Salary,
    Earnings,
    Gift,
    Repay,
    Save,
    Take,
    Other
}
