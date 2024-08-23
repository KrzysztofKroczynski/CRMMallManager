using Shared.Core.Entities;

namespace MallManager.Service;

public interface IRetailUnitLeaseApplicationService
{
    public Task LoadDataAsync();
    public IEnumerable<Lease> GetAllLeasesForRetailUnitId(int retailUnitId);
    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict);
    public Task<bool> CreateLeaseApplication(int surfaceClassDictId, int retailUnitPurposeId, DateTime? startDate, DateTime? endDate, string description);
}