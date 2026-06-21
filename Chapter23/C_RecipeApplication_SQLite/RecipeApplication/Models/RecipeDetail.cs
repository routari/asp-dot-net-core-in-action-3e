namespace RecipeApplication.Models;

public class RecipeDetailViewModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Method { get; init; }
    public DateTimeOffset LastModified { get; init; }

    public IEnumerable<Item> Ingredients { get; init; } = [];

    public class Item
    {
        public required string Name { get; init; }
        public required string Quantity { get; init; }
    }
}

