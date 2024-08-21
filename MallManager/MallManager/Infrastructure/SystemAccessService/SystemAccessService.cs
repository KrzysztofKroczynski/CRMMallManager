using Ardalis.Specification;
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

    public async Task<SystemAccess> CreateSystemAccess(AspNetUser aspNetUser, SystemDict systemDict)
    {
        var signupStatusByName = new SignupStatusByName("OCZEKUJĄCY");
        var pendingSignupStatus = await _signupStatusRepository.FirstOrDefaultAsync(signupStatusByName);

        if (pendingSignupStatus is null)
        {
            _logger.LogError("There is no status OCZEKUJĄCY in the database");
            // TODO: Instead of exceptions maybe create error codes?
            throw new Exception();
        }

        var systemAccess = new SystemAccess()
        {
            AspNetUsers = aspNetUser,
            SignupStatusDict = pendingSignupStatus,
            SystemDict = systemDict
        };

        try
        {
            await _systemAccessRepository.AddAsync(systemAccess);
            await _systemAccessRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return null;
        }
        
        _logger.LogInformation($"Successfully created pending approval system access for user {aspNetUser.UserName} to the {systemDict.Name} system");

        return systemAccess;
    }

    public bool DoesUserHasAccessToTheSystem(AspNetUser aspNetUser, SystemDict systemDict)
    {
        var existingValidSystemAccessesForUserToSystem =
            aspNetUser.SystemAccesses.Count(systemAccess => systemAccess.SystemDict.Equals(systemDict) && systemAccess.ValidUntil >= DateTime.Today);

        if (existingValidSystemAccessesForUserToSystem > 0)
        {
            _logger.LogInformation($"The {aspNetUser.UserName} AspNetUser has valid access to {systemDict.Name} system");
            return true;
        }

        _logger.LogInformation($"The {aspNetUser.UserName} AspNetUser does not hav valid access to the {systemDict.Name} system");
        return false;
    }

    public SystemAccess? GetValidSystemAccessOfUser(AspNetUser aspNetUser, SystemDict systemDict)
    {
        var validSystemAccess = aspNetUser.SystemAccesses.FirstOrDefault(systemAccess =>
            systemAccess.AspNetUsers.Equals(aspNetUser) && systemAccess.SystemDict.Equals(systemDict), null);

        return validSystemAccess;
    }
}