using System;
using RecipeApplication.Data;

namespace RecipeApplication.Models
{
    public class UpdateRecipeCommand : EditRecipeBase
    {
        public int Id { get; set; }

        public void UpdateRecipe(Recipe recipe)
        {
            recipe.Name = Name ?? string.Empty;
            recipe.TimeToCook = new TimeSpan(TimeToCookHrs, TimeToCookMins, 0);
            recipe.Method = Method ?? string.Empty;
            recipe.IsVegetarian = IsVegetarian;
            recipe.IsVegan = IsVegan;
        }
    }
}
