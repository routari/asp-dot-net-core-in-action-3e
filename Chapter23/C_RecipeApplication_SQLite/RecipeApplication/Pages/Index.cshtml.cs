using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;

namespace RecipeApplication.Pages;

public class IndexModel : PageModel
{
    private readonly RecipeService _service;
    public IEnumerable<RecipeSummaryViewModel> Recipes { get; private set; } = Array.Empty<RecipeSummaryViewModel>();

    public IndexModel(RecipeService service)
    {
        _service = service;
    }

    public async Task OnGet()
    {
        Recipes = await _service.GetRecipes();

    }
}
