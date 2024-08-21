using Shared.Core.Entities;

namespace MallManager.Infrastructure.Persistence.LeaseApplicationRepository;

public class LeaseApplicationRepository : ILeaseApplicationRepository
{
    private readonly MallManagerContext _mallManagerContext;

    public LeaseApplicationRepository(MallManagerContext mallManagerContext)
    {
        _mallManagerContext = mallManagerContext;
    }

    public async Task AddAsync(LeaseApplication leaseApplication, SurfaceClassDict surfaceClassDict,
        RetailUnitPurpose retailUnitPurpose)
    {
        using (var transaction = await _mallManagerContext.Database.BeginTransactionAsync())
        {
            try
            {
                await _mallManagerContext.LeaseApplications.AddAsync(leaseApplication);
                _mallManagerContext.SurfaceClassDicts.Update(surfaceClassDict);
                _mallManagerContext.RetailUnitPurposes.Update(retailUnitPurpose);

                await _mallManagerContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}