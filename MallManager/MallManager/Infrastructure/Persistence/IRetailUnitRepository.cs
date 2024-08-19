using Shared.Core.Entities;

namespace MallManager.Infrastructure.Persistence;

public interface IRetailUnitRepository
{
    public Task<ICollection<RetailUnit>> GetAllRetailUnits();
    public Task<ICollection<RetailUnitPurpose>> GetAllRetailUnitPurposes();
    public Task<ICollection<Lease>> GetAllLeasesOfRetailUnit(int retailUnitId);
    public Task<ICollection<SurfaceClassDict>> GetAllSurfaceClassDicts();
}