using Shared.Core.Entities;

namespace MallManager.Infrastructure.SystemAccessService;

public interface ISystemAccessService
{
    public Task<SystemAccess> CreateSystemAccess(string userId, Manager assignedManager, SystemDict systemDict);
    public Task<bool> DoesUserHasAccessToTheSystem(string userId, SystemDict systemDict);
    public Task<SystemAccess>? GetValidSystemAccessOfUser(string userId, SystemDict systemDict);
}