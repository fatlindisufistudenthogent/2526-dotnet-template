namespace Rise.Domain.Products;

public class Product : Entity
{
    private string _name = string.Empty;
    public required string Name
    {
        get => _name;
        set => _name = Guard.Against.NullOrWhiteSpace(value);
    }

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => _description = Guard.Against.NullOrWhiteSpace(value);
    }

    private string _test = string.Empty;
    public string Test
    {
        get => _test;
        set => _test = Guard.Against.NullOrWhiteSpace(value);
    }
}