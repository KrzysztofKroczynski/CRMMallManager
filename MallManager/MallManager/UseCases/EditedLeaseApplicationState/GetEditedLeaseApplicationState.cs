using Ardalis.Result;
using Ardalis.SharedKernel;
using MallManager.Infrastructure.UserState;
using Shared.Core.Entities;
using Shared.UseCases.GetUserState;

namespace MallManager.UseCases.EditedLeaseApplicationState;

public class GetEditedLeaseApplicationState : IQueryHandler<GetEditedLeaseApplicationStateQuery, Result<UserState<LeaseApplication>>>
{
    private readonly BaseStateService<LeaseApplication> _leaseApplicationStateService;

    public GetEditedLeaseApplicationState(BaseStateService<LeaseApplication> leaseApplicationStateService)
    {
        _leaseApplicationStateService = leaseApplicationStateService;
    } 
    
    public async Task<Result<UserState<LeaseApplication>>> Handle(GetEditedLeaseApplicationStateQuery request, CancellationToken cancellationToken)
    {
        return _leaseApplicationStateService.GetUserState(request.leaseApplicationId);
    }
}