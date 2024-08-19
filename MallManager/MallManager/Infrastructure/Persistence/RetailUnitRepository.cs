using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;

namespace MallManager.Infrastructure.Persistence;

public class RetailUnitRepository : IRetailUnitRepository
{
    private readonly MallManagerContext _context;

    public RetailUnitRepository(MallManagerContext context)
    {
        _context = context;
    }

    public async Task<ICollection<RetailUnit>> GetAllRetailUnits()
    {
        return await _context.RetailUnits.ToListAsync();
    }
    
    public async Task<ICollection<RetailUnitPurpose>> GetAllRetailUnitPurposes()
    {
        return await _context.RetailUnitPurposes.ToListAsync();
    }
    
    public async Task<ICollection<Lease>> GetAllLeasesOfRetailUnit(int retailUnitId)
    {
        return await _context.Leases
            .Where(lease => lease.RetailUnit.Id == retailUnitId).ToListAsync();
    }
    
    public async Task<ICollection<SurfaceClassDict>> GetAllSurfaceClassDicts()
    {
        return await _context.SurfaceClassDicts.ToListAsync();
    }
}