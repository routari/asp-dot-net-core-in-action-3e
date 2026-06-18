using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyConverter.Pages;

public class ConvertModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = new();

    public SelectListItem[] CurrencyCodes { get; } =
    {
            // Select Tag Helper only works with SelectListItem elements.
            new SelectListItem{Text="British Pounds", Value = "GBP"},
            new SelectListItem{Text="US Dollars", Value = "USD"},
            new SelectListItem{Text="Canadian Dollars", Value = "CAD"},
            new SelectListItem{Text="EU Euros", Value = "EUR"},
            new SelectListItem{Text="Japanese Yen", Value = "JPY"},
        };

    // public void OnGet()
    // {
    //     Input = new InputModel();
    // }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid && Input.CurrencyFrom == Input.CurrencyTo)
        {
            ModelState.AddModelError(string.Empty, "Cannot convert currency to itself");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Store the valid values somewhere (e.g. database), 
        // do the conversion etc

        return RedirectToPage("Success");
    }

    public class InputModel
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        [CurrencyCode("GBP", "USD", "CAD", "EUR", "JPY")]
        [Display(Name = "Currency From")]
        public string? CurrencyFrom { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [CurrencyCode("GBP", "USD", "CAD", "EUR", "JPY")]
        [Display(Name = "Currency To")]
        public string? CurrencyTo { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}