using Microsoft.EntityFrameworkCore;
using RecipeApplication.Data;
using RecipeApplication.Models;

namespace RecipeApplication;

public class RecipeService(AppDbContext context) //, ILogger<RecipeService> logger) // Doesn't seem to be used
{

    public async Task<IReadOnlyList<RecipeSummaryViewModel>> GetRecipes() =>
        await context.Recipes
            .Where(r => !r.IsDeleted)
            .Select(r => new RecipeSummaryViewModel
            {
                Id = r.RecipeId,
                Name = r.Name,
                TimeToCook = r.TimeToCook,
            })
            .ToListAsync();

    // Method/Fluent syntax (above) is preferred in modern C#
    // But Query syntax is more readable when doing joins or grouping
    // Example Query syntax - Which gets converted (desugared) to Fluent syntax prior to compilation
    //
    // await (
    //     from recipe in context.Recipes
    //     where !recipe.IsDeleted
    //     select new RecipeSummaryViewModel
    //     {
    //         Id = recipe.RecipeId,
    //         Name = recipe.Name,
    //         TimeToCook = x.TimeToCook,
    //     })
    //     .ToListAsync();


    public async Task<bool> DoesRecipeExistAsync(int id) =>
        await context.Recipes
            .Where(r => !r.IsDeleted)
            .Where(r => r.RecipeId == id)
            .AnyAsync();

    public async Task<RecipeDetailViewModel?> GetRecipeDetail(int id) =>
        await context.Recipes
            .Where(r => r.RecipeId == id)
            .Where(r => !r.IsDeleted)
            .Select(r => new RecipeDetailViewModel
            {
                Id = r.RecipeId,
                Name = r.Name,
                Method = r.Method,
                LastModified = r.LastModified,
                Ingredients = r.Ingredients
                .Select(item => new RecipeDetailViewModel.Item
                {
                    Name = item.Name,
                    Quantity = $"{item.Quantity} {item.Unit}"
                })
            })
            .SingleOrDefaultAsync();

    public async Task<UpdateRecipeCommand?> GetRecipeForUpdate(int recipeId)
    {
        return await context.Recipes
            .Where(x => x.RecipeId == recipeId)
            .Where(x => !x.IsDeleted)
            .Select(x => new UpdateRecipeCommand
            {
                Name = x.Name,
                Method = x.Method,
                TimeToCookHrs = x.TimeToCook.Hours,
                TimeToCookMins = x.TimeToCook.Minutes,
                IsVegan = x.IsVegan,
                IsVegetarian = x.IsVegetarian,
            })
            .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Create a new recipe
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns>The id of the new recipe</returns>
    public async Task<int> CreateRecipe(CreateRecipeCommand cmd)
    {
        var recipe = cmd.ToRecipe();
        context.Add(recipe);
        recipe.LastModified = DateTimeOffset.UtcNow;
        await context.SaveChangesAsync();
        return recipe.RecipeId;
    }

    /// <summary>
    /// Updateds an existing recipe
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns>The id of the new recipe</returns>
    public async Task UpdateRecipe(UpdateRecipeCommand cmd)
    {
        var recipe = await context.Recipes.FindAsync(cmd.Id);
        if (recipe == null) { throw new Exception("Unable to find the recipe"); }
        if (recipe.IsDeleted) { throw new Exception("Unable to update a deleted recipe"); }

        cmd.UpdateRecipe(recipe);
        recipe.LastModified = DateTimeOffset.UtcNow;
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Marks an existing recipe as deleted
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns>The id of the new recipe</returns>
    public async Task DeleteRecipe(int recipeId)
    {
        var recipe = await context.Recipes.FindAsync(recipeId);
        if (recipe is null) { throw new Exception("Unable to find recipe"); }

        recipe.IsDeleted = true;
        await context.SaveChangesAsync();
    }
}
