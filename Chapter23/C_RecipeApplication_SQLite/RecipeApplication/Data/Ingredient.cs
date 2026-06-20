namespace RecipeApplication.Data;

public class Ingredient
{
    public int IngredientId { get; set; }
    public int RecipeId { get; set; }
    public required string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public required string Unit { get; set; } = string.Empty;
}
