﻿using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Shared.Web.FormModels;

public class PersonalForm : MudForm
{
    [Required(ErrorMessage = "Imię jest wymagane.")]
    public string Name { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko jest wymagane.")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "PESEL jest wymagany.")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Format Pesel niepoprawny.")]
    public string Pesel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
    [MinimumAge(ErrorMessage = "Osoba musi mieć co najmniej 18 lat.")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
    [Phone(ErrorMessage = "Numer telefonu jest niepoprawny")]
    public string PhoneNumber { get; set; } = string.Empty;

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
    [RequiredIf(nameof(IsCompany), ErrorMessage = "NIP jest wymagany.")]
    public string? Nip { get; set; }

    [RequiredIf(nameof(IsCompany), ErrorMessage = "KRS jest wymagany.")]
    public string? Krs { get; set; }

    [RequiredIf(nameof(IsCompany), ErrorMessage = "Wielkość firmy jest wymagana.")]
    public string? CompanySize { get; set; }

    [RequiredIf(nameof(IsCompany), ErrorMessage = "Kapitał początkowy jest wymagany.")]
    public decimal? InitialCapital { get; set; }
}

public class RequiredIfAttribute : RequiredAttribute
{
    private readonly string _conditionProperty;

    public RequiredIfAttribute(string booleanPropertyName)
    {
        _conditionProperty = booleanPropertyName;
    }

    protected new ValidationResult? GetValidationResult(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_conditionProperty);
        if (property == null) return new ValidationResult($"Nie znaleziono właściwości '{_conditionProperty}'.");

        var conditionValue = property.GetValue(validationContext.ObjectInstance);

        if (conditionValue is true)
        {
            return base.GetValidationResult(value, validationContext);
        }

        return ValidationResult.Success;
    }
}
public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge = 18;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            
            if (age < _minimumAge)
            {
                return new ValidationResult($"Wiek musi wynosić co najmniej {_minimumAge} lat.");
            }
        }

        return ValidationResult.Success;
    }
}