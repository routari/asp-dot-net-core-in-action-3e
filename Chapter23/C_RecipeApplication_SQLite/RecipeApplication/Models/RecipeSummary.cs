﻿using RecipeApplication.Data;

namespace RecipeApplication.Models;

public class RecipeSummaryViewModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public TimeSpan TimeToCook { get; init; }
    public int NumberOfIngredients { get; init; }

    public string TimeToCookDisplay => $"{TimeToCook.Hours} hrs {TimeToCook.Minutes} mins";

    public static RecipeSummaryViewModel FromRecipe(Recipe recipe) => new()
    {
        Id = recipe.RecipeId,
        Name = recipe.Name,
        TimeToCook = recipe.TimeToCook,
    };
}

