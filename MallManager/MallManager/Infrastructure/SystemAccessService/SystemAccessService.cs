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

    public async Task<bool> CreateSystemAccess(SystemDict systemDict, AspNetUser aspNetUser)
    {
        var existingValidSystemAccessesForUserToSystem =
            aspNetUser.SystemAccesses.Count(systemAccess => systemAccess.SystemDict.Equals(systemDict) && systemAccess.ValidUntil >= DateTime.Today);

        if (existingValidSystemAccessesForUserToSystem > 0)
        {
            _logger.LogInformation($"This AspNetUser {aspNetUser.UserName} already has valid access to {systemDict.Name} system");
            return false;
        }

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
            return false;
        }

        return true;
    }
}