using Ardalis.Result;
using Shared.UseCases.GetUserState;

namespace MallManager.Infrastructure.UserState;

public abstract class BaseStateService<T> : IStateService<UserState<T>, T> where T : class, new()
{
    private readonly Dictionary<string, UserState<T>> _userStates = new();

    public Result<UserState<T>> GetUserState(string userId)
    {
        var exists = _userStates.TryGetValue(userId, out var userState);
        if (!exists)
        {
            userState = new UserState<T>() { State = new T() };
            _userStates.Add(userId, userState);
        }

        return userState!;
    }
}