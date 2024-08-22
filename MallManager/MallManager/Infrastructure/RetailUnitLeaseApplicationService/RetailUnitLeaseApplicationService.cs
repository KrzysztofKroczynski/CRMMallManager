using Ardalis.Specification;
using MallManager.Infrastructure.Persistence.LeaseApplicationRepository;
using MallManager.Service;
using Shared.Core.Entities;
using Shared.Core.Specifications;

namespace MallManager.Infrastructure.RetailUnitLeaseApplicationService;

public sealed class RetailUnitLeaseApplicationService : IRetailUnitLeaseApplicationService
{
    private readonly ILogger<RetailUnitLeaseApplicationService> _logger;
    private readonly ISystemAccessService _systemAccessService;
    
    private readonly IRepositoryBase<SignupStatusDict> _signupStatusRepository;
    private readonly ILeaseApplicationRepository _leaseApplicationRepository;
    private readonly IRepositoryBase<RetailUnit> _retailUnitRepository;
    private readonly IRepositoryBase<RetailUnitPurpose> _retailUnitPurposeRepository;
    private readonly IRepositoryBase<SurfaceClassDict> _surfaceClassDictRepository;
    private readonly IRepositoryBase<SystemDict> _systemDictRepository;

    public IEnumerable<RetailUnit> RetailUnits { get; private set; } = Enumerable.Empty<RetailUnit>();
    public IEnumerable<RetailUnitPurpose> RetailUnitPurposes { get; private set; } = Enumerable.Empty<RetailUnitPurpose>();
    public IEnumerable<SurfaceClassDict> SurfaceClassDicts { get; private set; } = Enumerable.Empty<SurfaceClassDict>();

    public RetailUnitLeaseApplicationService(ILogger<RetailUnitLeaseApplicationService> logger, ISystemAccessService systemAccessService, IRepositoryBase<SignupStatusDict> signupStatusRepository, ILeaseApplicationRepository leaseApplicationRepository,
        IRepositoryBase<RetailUnit> retailUnitRepository, IRepositoryBase<RetailUnitPurpose> retailUnitPurposeRepository, IRepositoryBase<SurfaceClassDict> surfaceClassDictRepository, IRepositoryBase<SystemDict> systemDictRepository)
    {
        _logger = logger;
        _systemAccessService = systemAccessService;
        
        _signupStatusRepository = signupStatusRepository;
        _leaseApplicationRepository = leaseApplicationRepository;
        _retailUnitRepository = retailUnitRepository;
        _retailUnitPurposeRepository = retailUnitPurposeRepository;
        _surfaceClassDictRepository = surfaceClassDictRepository;
        _systemDictRepository = systemDictRepository;
    }

    public async Task LoadDataAsync()
    {
        RetailUnits = await _retailUnitRepository.ListAsync();
        RetailUnitPurposes = await _retailUnitPurposeRepository.ListAsync();
        SurfaceClassDicts = await _surfaceClassDictRepository.ListAsync();
    }

    public IEnumerable<Lease> GetAllLeasesForRetailUnitId(int retailUnitId)
    {
        return RetailUnits.First(item => item.Id == retailUnitId).Leases;
    } 
    
    public string SurfaceClassDictsAsString(SurfaceClassDict surfaceClassDict)
    {
        return $"{surfaceClassDict.Name} ({surfaceClassDict.MinimalSurface} - {surfaceClassDict.MaximumSurface} m²)";
    }

    public async Task CreateLeaseApplication(int surfaceClassDictId, int retailUnitPurposeId, DateTime? startDate, DateTime? endDate, string description)
    {
        var pendingSignupStatus = await _signupStatusRepository.GetByIdAsync(1);
        if (pendingSignupStatus is null)
        {
            _logger.LogError("There is no status \"OCZEKUJĄCY\" in the database");
            // TODO: Instead of exceptions maybe create error codes?
            throw new ArgumentNullException();
        }
        _logger.LogInformation("Successfully fetched the \"OCZEKUJĄCY\" status");
        
        var systemDict = await _systemDictRepository.GetByIdAsync(1);
        if (systemDict is null)
        {
            _logger.LogError("There is no system \"WYNAJEM_LOKALI\" in the database");
            // TODO: Instead of exceptions maybe create error codes?
            throw new ArgumentNullException();
        }
        _logger.LogInformation("Successfully fetched the WYNAJEM_LOKALU system name");
        
        var retailUnitPurpose = RetailUnitPurposes.First(item => item.Id == retailUnitPurposeId);
        var surfaceClass = SurfaceClassDicts.First(item => item.Id == surfaceClassDictId);
        
        // PLACEHOLDER
        var aspNetUser = new AspNetUser
        {
            
        };

        var systemAccess = _systemAccessService.DoesUserHasAccessToTheSystem(aspNetUser, systemDict)
            ? _systemAccessService.GetValidSystemAccessOfUser(aspNetUser, systemDict)
            : await _systemAccessService.CreateSystemAccess(aspNetUser, systemDict);

        var leaseApplication = new LeaseApplication
        {
            DateStart = DateOnly.FromDateTime(startDate.Value),
            DateEnd = DateOnly.FromDateTime(endDate.Value),
            Description = description,
            SignupStatusDict = pendingSignupStatus,
            SystemAccess = systemAccess
        };

        leaseApplication.SurfaceClassDicts.Add(surfaceClass);
        leaseApplication.RetailUnitPurposes.Add(retailUnitPurpose);
        
        surfaceClass.LeaseApplications.Add(leaseApplication);
        retailUnitPurpose.LeaseApplications.Add(leaseApplication);
        
        try
        {
            await _leaseApplicationRepository.AddAsync(leaseApplication, surfaceClass, retailUnitPurpose);
            _logger.LogInformation($"Successfully created pending approval lease application for user {aspNetUser.UserName} to the {systemDict.Name} system");
        } catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
        }
    }
}
