﻿using Ardalis.Specification;
using MallManager.Infrastructure.SystemAccessService;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;

namespace MallManager.Infrastructure.RetailUnitLeaseApplicationService;

public sealed class RetailUnitLeaseApplicationService : IRetailUnitLeaseApplicationService
{
    private readonly ILogger<RetailUnitLeaseApplicationService> _logger;
    private readonly ISystemAccessService _systemAccessService;
    
    private readonly IRepositoryBase<SignupStatusDict> _signupStatusRepository;
    private readonly IRepositoryBase<LeaseApplication> _leaseApplicationRepository;
    private readonly IRepositoryBase<RetailUnit> _retailUnitRepository;
    private readonly IRepositoryBase<RetailUnitPurpose> _retailUnitPurposeRepository;
    private readonly IRepositoryBase<SurfaceClassDict> _surfaceClassDictRepository;
    private readonly IRepositoryBase<SystemDict> _systemDictRepository;

    public IEnumerable<RetailUnit> RetailUnits { get; private set; } = Enumerable.Empty<RetailUnit>();
    public IEnumerable<RetailUnitPurpose> RetailUnitPurposes { get; private set; } = Enumerable.Empty<RetailUnitPurpose>();
    public IEnumerable<SurfaceClassDict> SurfaceClassDicts { get; private set; } = Enumerable.Empty<SurfaceClassDict>();

    public RetailUnitLeaseApplicationService(ILogger<RetailUnitLeaseApplicationService> logger, ISystemAccessService systemAccessService, IRepositoryBase<SignupStatusDict> signupStatusRepository, IRepositoryBase<LeaseApplication> leaseApplicationRepository,
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

    public async Task<bool> CreateLeaseApplication(string userId, int surfaceClassDictId, int retailUnitPurposeId, DateTime? startDate,
        DateTime? endDate, string description)
    {
        var pendingSignupStatus = await _signupStatusRepository.GetByIdAsync(1);
        if (pendingSignupStatus is null)
        {
            _logger.LogError("There is no status \"Oczekujący\" in the database");
            return false;
            // TODO: Perhaps create error codes?
        }

        var systemDict = await _systemDictRepository.GetByIdAsync(1);
        if (systemDict is null)
        {
            _logger.LogError("There is no system \"Wynajmy\" in the database");
            return false;
            // TODO: Perhaps create error codes?
        }

        var retailUnitPurpose = RetailUnitPurposes.First(item => item.Id == retailUnitPurposeId);
        var surfaceClass = SurfaceClassDicts.First(item => item.Id == surfaceClassDictId);

        // PLACEHOLDERS
        var aspNetUserManager = new AspNetUser
        {
            Id = "1",
            UserName = null,
            NormalizedUserName = null,
            Email = null,
            NormalizedEmail = null,
            EmailConfirmed = false,
            PasswordHash = null,
            SecurityStamp = null,
            ConcurrencyStamp = null,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = false,
            AccessFailedCount = 0
        };

        var manager = new Manager()
        {
            Id = "1",
            IdNavigation = aspNetUserManager
        };

        SystemAccess systemAccess;
        if (await _systemAccessService.DoesUserHasAccessToTheSystem(userId, systemDict))
        {
            systemAccess = await _systemAccessService.GetValidSystemAccessOfUser(userId, systemDict);
        }
        else
        {
            try
            {
                systemAccess = await _systemAccessService.CreateSystemAccess(userId, manager, systemDict);
            }
            catch (DbUpdateException e)
            {
                return false;
            }
        }

        var newId = await GenerateNewId();
        var leaseApplication = new LeaseApplication
        {
            Id = newId,
            DateStart = DateOnly.FromDateTime(startDate.Value),
            DateEnd = DateOnly.FromDateTime(endDate.Value),
            Description = description,
            SignupStatusDict = pendingSignupStatus,
            SystemAccess = systemAccess
        };

        leaseApplication.RetailUnitPurposes.Add(retailUnitPurpose);
        leaseApplication.SurfaceClassDicts.Add(surfaceClass);

        try
        {
            await _leaseApplicationRepository.AddAsync(leaseApplication);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.StackTrace);
            return false;
        }
        _logger.LogInformation(
            $"Successfully created pending approval lease application for user with {userId} to the {systemDict.Name} system");
        return true;
    }
    
    private async Task<int> GenerateNewId()
    {
        var value = await _leaseApplicationRepository.CountAsync();
        return value + 1;
    }
}
