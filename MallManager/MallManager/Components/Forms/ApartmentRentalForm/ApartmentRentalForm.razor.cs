using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Core.Entities;
using Shared.Web.FormModels;

namespace MallManager.Components.Forms.ApartmentRentalForm;

public partial class ApartmentRentalForm : ComponentBase
{
    private RentalForm? _rentalForm = new();
    private IEnumerable<RetailUnit> _allRetailUnits = Enumerable.Empty<RetailUnit>();
    private IEnumerable<RetailUnit> _filteredRetailUnits = Enumerable.Empty<RetailUnit>();

    protected override async Task OnInitializedAsync()
    {
        await RetailUnitService.LoadDataAsync();
        _allRetailUnits = RetailUnitService.RetailUnits;
        _filteredRetailUnits = _allRetailUnits;
    }

    private void ApplyFilter()
    {
        /*Console.Beep();
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
        
        StateHasChanged();*/
    }

    private void OnValidSubmit(EditContext context)
    {
        Console.WriteLine("Poprawny");
    }
}