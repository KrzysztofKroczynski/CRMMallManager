using System.Data.Common;
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
    private readonly IRepositoryBase<AspNetUser> _aspNetUserRepository;

    public SystemAccessService(IRepositoryBase<SystemAccess> systemAccessRepository, IRepositoryBase<SignupStatusDict> signupStatusRepository, IRepositoryBase<AspNetUser> aspNetUserRepository, ILogger<SystemAccessService> logger)
    {
        _systemAccessRepository = systemAccessRepository;
        _signupStatusRepository = signupStatusRepository;
        _aspNetUserRepository = aspNetUserRepository;
        _logger = logger;
    }

    public async Task<SystemAccess> CreateSystemAccess(string userId, Manager assignedManager, SystemDict systemDict)
    {
        var pendingSignupStatus = await _signupStatusRepository.GetByIdAsync(1);

        if (pendingSignupStatus is null)
        {
            _logger.LogError("There is no status Oczekujący in the database");
            // TODO: Instead of exceptions maybe create error codes?
            throw new Exception();
        }
        
        _logger.LogInformation($"Creating system access for user with ID {userId}");
        var newId = await GenerateNewId();

        var systemAccess = new SystemAccess()
        {
            Id = newId,
            AspNetUsersId = userId,
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
        _logger.LogInformation($"Successfully created pending approval system access for user with {userId} to the {systemDict.Name} system");

        return systemAccess;
    }

    public async Task<bool> DoesUserHasAccessToTheSystem(string userId, SystemDict systemDict)
    {
        AspNetUser user = null;
        try
        {
            user = await _aspNetUserRepository.GetByIdAsync(int.Parse(userId));
        }
        catch (DbException e)
        {
            throw;
        }
        
        if (user is null || systemDict is null)
        {
            _logger.LogError($"User: {user}, System {systemDict}. The provided argument cannot be null");
            throw new ArgumentNullException();
        }
        
        var existingValidSystemAccessesForUserToSystem =
            user.SystemAccesses.Count(systemAccess => systemAccess.SystemDict.Equals(systemDict) && systemAccess.ValidUntil >= DateTime.Today);

        if (existingValidSystemAccessesForUserToSystem > 0)
        {
            _logger.LogInformation($"The {user.UserName} AspNetUser has valid access to {systemDict.Name} system");
            return true;
        }

        _logger.LogInformation($"The {user.UserName} AspNetUser does not have valid access to the {systemDict.Name} system");
        return false;
    }

    public async Task<SystemAccess>? GetValidSystemAccessOfUser(string userId, SystemDict systemDict)
    {
        AspNetUser user = null;
        try
        {
            user = await _aspNetUserRepository.GetByIdAsync(int.Parse(userId));
        }
        catch (DbException e)
        {
            throw;
        }
        
        var validSystemAccess = user.SystemAccesses.FirstOrDefault(systemAccess =>
            systemAccess.AspNetUsers.Equals(user) && systemAccess.SystemDict.Equals(systemDict), null);

        return validSystemAccess;
    }

    private async Task<int> GenerateNewId()
    {
        var value = await _systemAccessRepository.CountAsync() + 1;
        return value;
    }
}