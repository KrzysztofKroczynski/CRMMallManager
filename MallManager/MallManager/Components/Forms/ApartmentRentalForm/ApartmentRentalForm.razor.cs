using System.Collections;
using MallManager.Infrastructure.Persistence;
using MallManager.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Shared.Core.Entities;
using Shared.Web.FormModels;

namespace MallManager.Components.Forms.ApartmentRentalForm;

public partial class ApartmentRentalForm : ComponentBase
{
    [Parameter] public RentalForm RentalForm { get; set; }
    private EditForm _editForm;
    
    private IEnumerable<RetailUnit> _allRetailUnits;
    private IEnumerable<RetailUnit> _filteredRetailUnits;
    private string _errorMessage;

    protected override void OnInitialized()
    {
        _allRetailUnits = RetailUnitService.RetailUnits;
    }

    private void ApplyFilter()
    {
        _filteredRetailUnits = RetailUnitService.FilterRetailUnits(RentalForm.SurfaceClassDict,
            RentalForm.RetailUnitPurpose, DateOnly.FromDateTime(RentalForm.StartDate.Value), DateOnly.FromDateTime(RentalForm.EndDate.Value)).Result;
        
        StateHasChanged();
    }
    
    private string ValidateStartDate(DateOnly? startDate)
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

        return RentalForm.IsValid ? true : false;
    }
}