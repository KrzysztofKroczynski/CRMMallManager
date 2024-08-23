using Shared.Core.Entities;

namespace MallManager.Service;

public interface ISystemAccessService
{
    public Task<SystemAccess> CreateSystemAccess(AspNetUser aspNetUser, Manager assignedManager, SystemDict systemDict);
    public bool DoesUserHasAccessToTheSystem(AspNetUser aspNetUser, SystemDict systemDict);
    public SystemAccess? GetValidSystemAccessOfUser(AspNetUser aspNetUser, SystemDict systemDict);
}