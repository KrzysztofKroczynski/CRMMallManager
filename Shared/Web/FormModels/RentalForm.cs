using System.ComponentModel.DataAnnotations;
using System.Data;
using MudBlazor;
using MudBlazor.Extensions;
using Shared.Core.Entities;

namespace Shared.Web.FormModels;

public class RentalForm : MudForm
{
    [Required(ErrorMessage = "Metraż jest wymagany")]
    public string SurfaceClassDictName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Typ działalności jest wymagany")]
    public string RetailUnitPurposeName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Data \"od\" jest wymagana")]
    [DateEarlierThan("EndDate", ErrorMessage = "Data \"od\" musi być wcześniejsza niż data \"do\".")]
    [NotEarlierThanToday(ErrorMessage = "Data \"od\" nie może być wcześniejsza niż data dzisiejsza")]
    public DateTime? StartDate { get; set; }
    
    [Required(ErrorMessage = "Data \"do\" jest wymagana")]
    [DateLaterThan("StartDate", ErrorMessage = "Data \"do\" musi być późniejsza niż data \"od\".")]
    [NotEarlierThanToday(ErrorMessage = "Data \"do\" nie może być wcześniejsza niż data dzisiejsza")]
    public DateTime? EndDate { get; set; }
    
    [Required(ErrorMessage = "Opis jest wymagany")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Maksymalnie może zawierać 1000 znaków")]
    public string BusinessDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lokal jest wymagany")]
    public int RentalUnitId { get; set; }
}

public class DateLaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateLaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = (DateTime?)value;

        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
        var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

        if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}

public class DateEarlierThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateEarlierThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = (DateTime?)value;

        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
        var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

        if (currentValue.HasValue && comparisonValue.HasValue && currentValue >= comparisonValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}

public class NotEarlierThanTodayAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime)
        {
            DateTime dateValue = (DateTime)value;
            if (dateValue.Date < DateTime.Today)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        else
        {
            return new ValidationResult("Invalid data type");
        }
        return ValidationResult.Success;
    }
}