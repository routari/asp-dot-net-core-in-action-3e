using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;

namespace RecipeApplication.Pages;

public class IndexModel(RecipeService service) : PageModel
{
    public IReadOnlyList<RecipeSummaryViewModel> Recipes { get; private set; } = [];

    public async Task OnGet()
    {
        Recipes = await service.GetRecipes();

    }
}
