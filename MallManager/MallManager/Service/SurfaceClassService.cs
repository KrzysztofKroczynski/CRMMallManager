using Ardalis.SharedKernel;
using MallManager.Infrastructure.Persistence;
using Shared.Core.Entities;

namespace MallManager.Service;

public class SurfaceClassService
{
    private readonly IRepository<SurfaceClassDict> _repository;
    public IEnumerable<SurfaceClassDict> SurfaceClassDicts { get; set; }

    public SurfaceClassService(IRepository<SurfaceClassDict> repository)
    {
        _repository = repository;
        SurfaceClassDicts = _repository.ListAsync().Result;
    }

    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict)
    {
        return surfaceClassDict.Name + "(" + surfaceClassDict.MinimalSurface + " - " +
                   surfaceClassDict.MaximumSurface + "m kwadratowych)";
    }
}