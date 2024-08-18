using Shared.Core.Entities;

namespace MallManager.Service;

public interface IRetailUnitService
{
    public Task LoadDataAsync();
    public IEnumerable<RetailUnit> FilterRetailUnits(SurfaceClassDict surfaceClassDict,
        RetailUnitPurpose retailUnitPurpose, DateOnly? startDate, DateOnly? endDate);

    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict);
}