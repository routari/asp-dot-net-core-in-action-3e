using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter;

public class CurrencyCodeAttribute(params string[] allowedCodes) : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        var code = value as string;
        if (code == null || !allowedCodes.Contains(code))
        {
            return new ValidationResult("Not a valid currency code");
        }

        return ValidationResult.Success;
    }
}

