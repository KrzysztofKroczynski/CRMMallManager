using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Specifications;

namespace MallManager.Service;

public sealed class SystemAccessService : ISystemAccessService
{
    private readonly ILogger<SystemAccessService> _logger;
    private readonly IRepositoryBase<SystemAccess> _systemAccessRepository;
    private readonly IRepositoryBase<SignupStatusDict> _signupStatusRepository;

    public SystemAccessService(IRepositoryBase<SystemAccess> systemAccessRepository, IRepositoryBase<SignupStatusDict> signupStatusRepository, ILogger<SystemAccessService> logger)
    {
        _systemAccessRepository = systemAccessRepository;
        _signupStatusRepository = signupStatusRepository;
        _logger = logger;
    }

    public async Task<SystemAccess> CreateSystemAccess(AspNetUser aspNetUser, Manager assignedManager, SystemDict systemDict)
    {
        var pendingSignupStatus = await _signupStatusRepository.GetByIdAsync(1);

        if (pendingSignupStatus is null)
        {
            _logger.LogError("There is no status Oczekujący in the database");
            // TODO: Instead of exceptions maybe create error codes?
            throw new Exception();
        }
        
        _logger.LogInformation($"Creating system access for user {aspNetUser.UserName}");
        var newId = await GenerateNewId();

        var systemAccess = new SystemAccess()
        {
            Id = newId,
            AspNetUsers = aspNetUser,
            SignupStatusDict = pendingSignupStatus,
            SystemDict = systemDict,
            AssignedManager = assignedManager
        };

        try
        {
            await _systemAccessRepository.AddAsync(systemAccess);
            await _systemAccessRepository.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.StackTrace);
            throw;
        }
        _logger.LogInformation($"Successfully created pending approval system access for user {aspNetUser.UserName} to the {systemDict.Name} system");

        return systemAccess;
    }

    public bool DoesUserHasAccessToTheSystem(AspNetUser aspNetUser, SystemDict systemDict)
    {
        if (aspNetUser is null || systemDict is null)
        {
            _logger.LogError($"User: {aspNetUser}, System {systemDict}. The provided argument cannot be null");
            throw new ArgumentNullException();
        }
        
        var existingValidSystemAccessesForUserToSystem =
            aspNetUser.SystemAccesses.Count(systemAccess => systemAccess.SystemDict.Equals(systemDict) && systemAccess.ValidUntil >= DateTime.Today);

        if (existingValidSystemAccessesForUserToSystem > 0)
        {
            _logger.LogInformation($"The {aspNetUser.UserName} AspNetUser has valid access to {systemDict.Name} system");
            return true;
        }

        _logger.LogInformation($"The {aspNetUser.UserName} AspNetUser does not have valid access to the {systemDict.Name} system");
        return false;
    }

    public SystemAccess? GetValidSystemAccessOfUser(AspNetUser aspNetUser, SystemDict systemDict)
    {
        var validSystemAccess = aspNetUser.SystemAccesses.FirstOrDefault(systemAccess =>
            systemAccess.AspNetUsers.Equals(aspNetUser) && systemAccess.SystemDict.Equals(systemDict), null);

        return validSystemAccess;
    }

    private async Task<int> GenerateNewId()
    {
        var value = await _systemAccessRepository.CountAsync() + 1;
        return value;
    }
}