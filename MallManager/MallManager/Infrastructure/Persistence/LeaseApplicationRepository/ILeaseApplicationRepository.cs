using Shared.Core.Entities;

namespace MallManager.Infrastructure.Persistence.LeaseApplicationRepository;

public interface ILeaseApplicationRepository
{
    public Task AddAsync(LeaseApplication leaseApplication, SurfaceClassDict surfaceClassDict,
        RetailUnitPurpose retailUnitPurpose);
}