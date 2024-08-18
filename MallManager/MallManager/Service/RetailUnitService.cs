using MallManager.Infrastructure.Persistence;
using Shared.Core.Entities;

namespace MallManager.Service;

public sealed class RetailUnitService : IRetailUnitService
{
    private readonly IRetailUnitRepository _repository;

    public IEnumerable<RetailUnit> RetailUnits { get; private set; } = Enumerable.Empty<RetailUnit>();
    public IEnumerable<RetailUnitPurpose> RetailUnitPurposes { get; private set; } = Enumerable.Empty<RetailUnitPurpose>();
    public IEnumerable<SurfaceClassDict> SurfaceClassDicts { get; private set; } = Enumerable.Empty<SurfaceClassDict>();

    public RetailUnitService(IRetailUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task LoadDataAsync()
    {
        RetailUnits = await _repository.GetAllRetailUnits();
        RetailUnitPurposes = await _repository.GetAllRetailUnitPurposes();
        SurfaceClassDicts = await _repository.GetAllSurfaceClassDicts();
    }

    public IEnumerable<RetailUnit> FilterRetailUnits(SurfaceClassDict surfaceClassDict,
        RetailUnitPurpose retailUnitPurpose, DateOnly? startDate, DateOnly? endDate)
    {
        return RetailUnits.Where(item =>
            !item.Leases.Any(lease => lease.StartDate < endDate && lease.EndDate > startDate)
            && item.LocalSurfaceArea >= surfaceClassDict.MinimalSurface
            && item.LocalSurfaceArea <= surfaceClassDict.MaximumSurface
            && item.RetailUnitPurpose.Equals(retailUnitPurpose)).ToList();
    }
    
    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict)
    {
        return $"{surfaceClassDict.Name} ({surfaceClassDict.MinimalSurface} - {surfaceClassDict.MaximumSurface} m²)";
    }
}
