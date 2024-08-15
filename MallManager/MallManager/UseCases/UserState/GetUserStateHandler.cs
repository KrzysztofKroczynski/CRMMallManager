using Ardalis.Result;
using Ardalis.SharedKernel;
using MallManager.Infrastructure.UserState;
using Shared.UseCases.GetUserState;
using Shared.Web.FormModels;

namespace MallManager.UseCases.UserState;

public class GetUserStateHandler : IQueryHandler<GetUserPersonalFormStateQuery, Result<UserState<PersonalForm>>>
{
    private readonly BaseStateService<PersonalForm> _personalFormStateService;

    public GetUserStateHandler(BaseStateService<PersonalForm> personalFormStateService)
    {
        _personalFormStateService = personalFormStateService;
    }

    public async Task<Result<UserState<PersonalForm>>> Handle(GetUserPersonalFormStateQuery request,
        CancellationToken cancellationToken)
    {
        return _personalFormStateService.GetUserState(request.UserId);
    }
}