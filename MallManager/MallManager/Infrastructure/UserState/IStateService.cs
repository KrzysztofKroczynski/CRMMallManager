using Ardalis.Result;
using Shared.UseCases.GetUserState;

namespace MallManager.Infrastructure.UserState;

public interface IStateService<TState, TV> where TState : UserState<TV> where TV : class
{
    public Result<TState> GetUserState(string userId);
}