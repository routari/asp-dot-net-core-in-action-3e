using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;

namespace RecipeApplication.Pages.Recipes;

public class CreateModel(RecipeService service) : PageModel
{
    [BindProperty]
    public required CreateRecipeCommand Input { get; set; }

    public void OnGet()
    {
        Input = new CreateRecipeCommand();
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (ModelState.IsValid)
            {
                var id = await service.CreateRecipe(Input);
                return RedirectToPage("View", new { id = id });
            }
        }
        catch (Exception)
        {
            // TODO: Log error
            // Add a model-level error by using an empty string key
            ModelState.AddModelError(
                string.Empty,
                "An error occured saving the recipe"
                );
        }

        //If we got to here, something went wrong
        return Page();
    }
}
