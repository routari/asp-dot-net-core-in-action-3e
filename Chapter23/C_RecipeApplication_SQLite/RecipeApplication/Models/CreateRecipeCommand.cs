using RecipeApplication.Data;

namespace RecipeApplication.Models;

public class CreateRecipeCommand : EditRecipeBase
{
    public IList<CreateIngredientCommand> Ingredients { get; set; } = new List<CreateIngredientCommand>();

    public Recipe ToRecipe()
    {
        return new Recipe
        {
            Name = Name ?? string.Empty,
            TimeToCook = new TimeSpan(TimeToCookHrs, TimeToCookMins, 0),
            Method = Method ?? string.Empty,
            IsVegetarian = IsVegetarian,
            IsVegan = IsVegan,
            Ingredients = Ingredients.Select(x => x.ToIngredient()).ToList()
        };
    }
}

