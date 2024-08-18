using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Core.Entities;
using Shared.Web.FormModels;

namespace MallManager.Components.Forms.ApartmentRentalForm;

public partial class ApartmentRentalForm : ComponentBase
{
    // [Parameter] public RentalForm? RentalForm { get; set; }
    public RentalForm RentalForm = new();
    private EditForm? _editForm;
    
    private IEnumerable<RetailUnit> _allRetailUnits = Enumerable.Empty<RetailUnit>();
    private IEnumerable<RetailUnit> _filteredRetailUnits = Enumerable.Empty<RetailUnit>();
    private string? _errorMessage;

    protected override async Task OnInitializedAsync()
    {
        await RetailUnitService.LoadDataAsync();
        _allRetailUnits = RetailUnitService.RetailUnits;
        _filteredRetailUnits = _allRetailUnits;
    }

    private void ApplyFilter()
    {
        Console.Beep();
        if (RentalForm.SurfaceClassDict != null && RentalForm.RetailUnitPurpose != null && 
            RentalForm.StartDate.HasValue && RentalForm.EndDate.HasValue)
        {
            _filteredRetailUnits = RetailUnitService.FilterRetailUnits(
                RentalForm.SurfaceClassDict,
                RentalForm.RetailUnitPurpose,
                DateOnly.FromDateTime(RentalForm.StartDate.Value),
                DateOnly.FromDateTime(RentalForm.EndDate.Value)
            );
        }
        else
        {
            _filteredRetailUnits = _allRetailUnits;
        }
        
        StateHasChanged();
    }
    
    private string? ValidateStartDate(DateOnly? startDate)
    {
        if (RentalForm.EndDate.HasValue && startDate.HasValue && startDate >= DateOnly.FromDateTime(RentalForm.EndDate.Value))
        {
            _errorMessage = "Data 'Od' nie może być późniejsza lub równa dacie 'Do'.";
            return _errorMessage;
        }

        _errorMessage = null;
        return null;
    }

    private void ValidateEndDate(ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value.ToString(), out var endDate))
        {
            RentalForm.EndDate = endDate;
            if (RentalForm.StartDate.HasValue && endDate <= RentalForm.StartDate)
            {
                _errorMessage = "Data 'Do' nie może być wcześniejsza lub równa dacie 'Od'.";
            }
            else
            {
                _errorMessage = null;
            }
        }
    }

    public async Task<bool> IsFormValid()
    {
        await RentalForm.Validate();

        return RentalForm.IsValid;
    }

    private void OnValidSubmit(EditContext context)
    {
        Console.WriteLine("Poprawny");
    }
}