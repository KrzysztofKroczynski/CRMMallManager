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

    public async Task<IEnumerable<Lease>> GetAllLeasesForRetailUnitId(int retailUnitId)
    {
        return await _repository.GetAllLeasesOfRetailUnit(retailUnitId);
    } 
    
    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict)
    {
        return $"{surfaceClassDict.Name} ({surfaceClassDict.MinimalSurface} - {surfaceClassDict.MaximumSurface} m²)";
    }
}
