namespace MallManager.Infrastructure.ManageRequestsService;

public interface IManageRequestsService
{
    public Task LoadAspNetUserRequestsAsync(int userId);
}