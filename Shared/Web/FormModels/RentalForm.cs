using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Shared.Web.FormModels;

public class RentalForm : INotifyPropertyChanged
{
    private int _surfaceClassDictId;
    private int _retailUnitPurposeId;
    private DateTime? _startDate;
    private DateTime? _endDate;
    
    [Required(ErrorMessage = "Metraż jest wymagany")]
    public int SurfaceClassDictId
    {
        get => _surfaceClassDictId;
        set
        {
            if (_surfaceClassDictId != value)
            {
                _surfaceClassDictId = value;
                OnPropertyChanged();
            }
        }
    }
    
    [Required(ErrorMessage = "Typ działalności jest wymagany")]
    public int RetailUnitPurposeId 
    {
        get => _retailUnitPurposeId;
        set
        {
            if (_retailUnitPurposeId != value)
            {
                _retailUnitPurposeId = value;
                OnPropertyChanged();
            }
        }
    }
    
    [Required(ErrorMessage = "Data \"od\" jest wymagana")]
    [DateEarlierThan("EndDate", ErrorMessage = "Data \"od\" musi być wcześniejsza niż data \"do\".")]
    [NotEarlierThanToday(ErrorMessage = "Data \"od\" nie może być wcześniejsza niż data dzisiejsza")]
    public DateTime? StartDate 
    {
        get => _startDate;
        set
        {
            if (_startDate != value)
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
    }
    
    [Required(ErrorMessage = "Data \"do\" jest wymagana")]
    [DateLaterThan("StartDate", ErrorMessage = "Data \"do\" musi być późniejsza niż data \"od\".")]
    [NotEarlierThanToday(ErrorMessage = "Data \"do\" nie może być wcześniejsza niż data dzisiejsza")]
    public DateTime? EndDate 
    {
        get => _endDate;
        set
        {
            if (_endDate != value)
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
    }
    
    [Required(ErrorMessage = "Opis jest wymagany")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Maksymalnie może zawierać 1000 znaków")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lokal jest wymagany")]
    public int RentalUnitId { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


// TODO: Perhaps move these validation attributes to some namespace
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