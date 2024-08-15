using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.UseCases.GetUserState;
using Shared.Web;

namespace MallManager.Components.Pages.Common.PersonalInfo;

public sealed partial class PersonalInfo : ComponentBase, IDisposable
{
    private string[] _companySizes =
    {
        "Mała", "Średnia", "Duża"
    };

    private PersonalForm _model = new();
    private Action? _onChangeHandler;

    private UserState<PersonalForm> State { get; set; }

    public void Dispose()
    {
        State.OnChange -= _onChangeHandler;
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _onChangeHandler = async () => await InvokeAsync(StateHasChanged);

        State = await Mediator.Send(new GetUserPersonalFormStateQuery("2173"));
        _model = State.State!;

        State.OnChange += _onChangeHandler;
    }

    private void OnValidSubmit(EditContext context)
    {
    }
}