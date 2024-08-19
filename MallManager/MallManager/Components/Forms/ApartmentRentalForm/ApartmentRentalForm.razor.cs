using System.ComponentModel;
using Ardalis.Specification;
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
    
    private int _previousRetailUnitPurposeId = 0;
    private int _previousSurfaceClassDictId = 0;
    private DateTime? _previousStartDate = DateTime.Today;
    private DateTime? _previousEndDate = DateTime.Today;

    protected override async Task OnInitializedAsync()
    {
        await RetailUnitService.LoadDataAsync();
        _allRetailUnits = RetailUnitService.RetailUnits;
        _filteredRetailUnits = _allRetailUnits;
        _rentalForm.PropertyChanged += FormModelPropertyChanged;
    }
    
    private void FormModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        ApplyFilter();
    }
    
    public void Dispose()
    {
        _rentalForm.PropertyChanged -= FormModelPropertyChanged;
    }
    
    // TODO: Refactor this (perhaps move to service, or divide into smaller methods)
    private void ApplyFilter()
    {
        _filteredRetailUnits = _allRetailUnits;
        
        if (_rentalForm.RetailUnitPurposeId != null && _rentalForm.RetailUnitPurposeId != _previousRetailUnitPurposeId)
        {
            _filteredRetailUnits = _filteredRetailUnits.Where(retailUnit => 
                retailUnit.RetailUnitPurposeId == _rentalForm.RetailUnitPurposeId)
                .ToList();
            
            _previousRetailUnitPurposeId = _rentalForm.RetailUnitPurposeId;
        }
        
        if (_rentalForm.SurfaceClassDictId != null && _rentalForm.SurfaceClassDictId != _previousSurfaceClassDictId)
        {
            _filteredRetailUnits = _filteredRetailUnits.Where(retailUnit => 
                    RetailUnitService.SurfaceClassDicts.Any(surfaceClassDict => 
                        surfaceClassDict.Id == _rentalForm.SurfaceClassDictId 
                        && retailUnit.LocalSurfaceArea >= surfaceClassDict.MinimalSurface 
                        && retailUnit.LocalSurfaceArea <= surfaceClassDict.MaximumSurface)
                    )
                .ToList();
            
            _previousSurfaceClassDictId = _rentalForm.SurfaceClassDictId;
        }

        if (_rentalForm.StartDate != null && _rentalForm.StartDate != _previousStartDate)
        {
            var startDateAsDateOnly = new DateOnly?(DateOnly.FromDateTime(_rentalForm.StartDate.Value));
            
            _filteredRetailUnits = _filteredRetailUnits.Where(retailUnit =>
                    RetailUnitService.GetAllLeasesForRetailUnitId(retailUnit.Id).Result.Where(lease =>
                        startDateAsDateOnly < lease.EndDate).ToList()
                    .Count == 0)
                .ToList();

            _previousStartDate = _rentalForm.StartDate;
        }
        
        if (_rentalForm.EndDate != null && _rentalForm.EndDate != _previousEndDate)
        {
            var endDateAsDateOnly = new DateOnly?(DateOnly.FromDateTime(_rentalForm.EndDate.Value));
            
            _filteredRetailUnits = _filteredRetailUnits.Where(retailUnit =>
                    RetailUnitService.GetAllLeasesForRetailUnitId(retailUnit.Id).Result.Where(lease =>
                            endDateAsDateOnly > lease.StartDate).ToList()
                        .Count == 0)
                .ToList();

            _previousEndDate = _rentalForm.EndDate;
        }
        
        StateHasChanged();
    }
    

    private void OnValidSubmit(EditContext context)
    {
        // TODO: Add logic responsible for sending and navigating to convirmation page
        Console.WriteLine("Poprawny");
    }
}