using System.Collections;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;

namespace MallManager.Infrastructure.Persistence;

public class RetailUnitRepository
{
    private readonly MallManagerContext _context;

    public RetailUnitRepository(MallManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RetailUnit>> GetAllRetailUnits()
    {
        return await _context.RetailUnits.ToListAsync();
    }
    
    public async Task<IEnumerable<RetailUnitPurpose>> GetAllRetailUnitPurposes()
    {
        return await _context.RetailUnitPurposes.ToListAsync();
    }
    
    public async Task<IEnumerable<Lease>> GetAllLeasesOfRetailUnit(RetailUnit retailUnit)
    {
        return await _context.Leases
            .Where(lease => lease.RetailUnit.Equals(retailUnit))
            .ToListAsync();
    }
}