using Shared.Core.Entities;

namespace MallManager.Service;

public interface ISystemAccessService
{
    public Task<bool> CreateSystemAccess(SystemDict systemDict, AspNetUser aspNetUser);
}