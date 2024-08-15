namespace Shared.UseCases.GetUserState;

public class UserState<T> where T : class
{
    private T? _state;

    public T? State
    {
        get => _state;
        set
        {
            _state = value;
            NotifyStateChanged();
        }
    }

    public event Action OnChange = delegate { };

    public void NotifyStateChanged() => Task.Run(() => OnChange?.Invoke());
}