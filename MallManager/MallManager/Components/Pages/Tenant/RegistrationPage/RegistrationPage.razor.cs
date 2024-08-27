using MallManager.Components.Forms.ApartmentRentalForm;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudExtensions;
using Shared.Web.FormModels;

namespace MallManager.Components.Pages.Tenant.RegistrationPage;

// TODO: Perhaps move this enum to a seperate file
internal enum PurposeOfTheContractType
{
    APARTMENT_RENTAL,
    ADVERTISING_SPACE_RENTAL,
    EVENT_ORGANIZATION,
    OTHER
}

public partial class RegistrationPage : ComponentBase
{
    private MudStepperExtended _stepper = new();
    private PurposeOfTheContractType _currentPurposeOfTheContractType = PurposeOfTheContractType.OTHER;
    private bool _loading;
    
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/Login");
            return;
        }
    }

    private async Task<bool> CheckChange(StepChangeDirection direction, int targetIndex)
    {
        if (direction == StepChangeDirection.Backward)
        {
            var previousIndex = _stepper.GetActiveIndex() - 1;
            _stepper.SetStepStatus(previousIndex, StepStatus.Continued);
            return false;
        }
        
        return false;
    } 

    private async Task RenderApartmentRentalForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.APARTMENT_RENTAL;
        await MoveToNextStep();
    }

    private async Task RenderAdvertisingSpaceRentalForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.ADVERTISING_SPACE_RENTAL;
        await MoveToNextStep();
    }
    
    private async Task RenderEventOrganizationForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.EVENT_ORGANIZATION;
        await MoveToNextStep();
    } 
    
    private async Task RenderOtherPurposeForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.OTHER;
        await MoveToNextStep();
    }
    
    private async Task MoveToNextStep()
    {
        var currentStepIndex = _stepper.GetActiveIndex();
        
        _loading = true;
        StateHasChanged();
        
        await _stepper.CompleteStep(currentStepIndex);
        await _stepper.SetActiveStepByIndex(currentStepIndex + 1);
        
        await Task.Delay(1000);
        
        _loading = false;
        StateHasChanged();
    }
}