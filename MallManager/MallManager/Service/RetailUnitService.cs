using System.Collections;
using Ardalis.SharedKernel;
using MallManager.Infrastructure.Persistence;
using Shared.Core.Entities;

namespace MallManager.Service;

public class RetailUnitService
{
    private readonly IRepository<RetailUnit> _repositoryRetailUnits;
    private readonly IRepository<SurfaceClassDict> _repositorySurfaceClassDict;

    public IEnumerable<RetailUnit> RetailUnits { get; set; }
    public List<SurfaceClassDict> RetailUnitPurposes { get; set; }

    public RetailUnitService(IRepository<RetailUnit> repository1, IRepository<SurfaceClassDict> repository2)
    {
        _repositoryRetailUnits = repository1;
        _repositorySurfaceClassDict = repository2;
        RetailUnits = _repositoryRetailUnits.ListAsync().Result;
        RetailUnitPurposes = _repositorySurfaceClassDict.ListAsync().Result;
    }

    public async Task<IEnumerable<RetailUnit>> FilterRetailUnits(SurfaceClassDict surfaceClassDict,
        RetailUnitPurpose retailUnitPurpose, DateOnly? startDate, DateOnly? endDate)
    {
        return RetailUnits.Where(item => !item.Leases.Any(lease => lease.StartDate < endDate && lease.EndDate > startDate) 
                                         && item.LocalSurfaceArea >= surfaceClassDict.MinimalSurface 
                                         && item.LocalSurfaceArea <= surfaceClassDict.MaximumSurface 
                                         && item.RetailUnitPurpose.Equals(retailUnitPurpose)).ToList();
    }
}