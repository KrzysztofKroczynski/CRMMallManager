using Shared.Core.Entities;

namespace MallManager.Service;

public interface IRetailUnitService
{
    public Task LoadDataAsync();
    public Task<IEnumerable<Lease>> GetAllLeasesForRetailUnitId(int retailUnitId);
    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict);
}