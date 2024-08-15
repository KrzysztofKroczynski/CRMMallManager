using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Shared.Web;

public class PersonalForm : MudForm
{
    [Required(ErrorMessage = "Imię jest wymagane.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Drugie imię jest wymagane.")]
    public string SecondName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko jest wymagane.")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
    public int PhoneNumber { get; set; }

    [Required(ErrorMessage = "Kraj jest wymagany.")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "Miasto jest wymagane.")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ulica jest wymagana.")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numer budynku jest wymagany.")]
    public string BuildingNumber { get; set; } = string.Empty;

    public string LocalNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
    public string PostalCode { get; set; } = string.Empty;

    public string AdditionalAddressInfo { get; set; } = string.Empty;

    // Checkbox do określenia, czy użytkownik jest firmą
    public bool IsCompany { get; set; }

    // Pola związane z firmą, wymagane tylko, jeśli IsCompany = true
    [PersonalInfoCompanyInfo(nameof(IsCompany),
        true, ErrorMessage = "NIP jest wymagany.")]
    public string? Nip { get; set; }

    [PersonalInfoCompanyInfo(nameof(IsCompany),
        true, ErrorMessage = "KRS jest wymagany.")]
    public string? Krs { get; set; }

    [PersonalInfoCompanyInfo(nameof(IsCompany),
        true, ErrorMessage = "Wielkość firmy jest wymagana.")]
    public string? CompanySize { get; set; }

    [PersonalInfoCompanyInfo(nameof(IsCompany),
        true, ErrorMessage = "Kapitał początkowy jest wymagany.")]
    public decimal? InitialCapital { get; set; }
}

public class PersonalInfoCompanyInfoAttribute : ValidationAttribute
{
    private readonly string _conditionProperty;
    private readonly object _expectedValue;

    public PersonalInfoCompanyInfoAttribute(string conditionProperty, object expectedValue)
    {
        _conditionProperty = conditionProperty;
        _expectedValue = expectedValue;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_conditionProperty);
        if (property == null)
        {
            return new ValidationResult($"Nie znaleziono właściwości '{_conditionProperty}'.");
        }

        var conditionValue = property.GetValue(validationContext.ObjectInstance);

        if (conditionValue?.ToString() == _expectedValue.ToString() && value is null)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}