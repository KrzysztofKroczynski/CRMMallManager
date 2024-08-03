using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudExtensions;

namespace MallManager.Components.Pages;

enum FormType
{
    APARTMENT_RENTAL,
    ADVERTISING_SPACE_RENTAL,
    EVENT_ORGANIZATION,
    OTHER
}

public partial class RegistrationPage : ComponentBase
{
    MudStepperExtended _stepper = new();
    private MudForm _form = new();
    private FormType _currentFormType = FormType.OTHER;
    private bool _loading;
    
    private async Task<bool> CheckChange(StepChangeDirection direction, int targetIndex)
    {
        if (direction == StepChangeDirection.Backward)
        {
            var previousIndex = _stepper.GetActiveIndex() - 1;
            _stepper.SetStepStatus(previousIndex, StepStatus.Continued);
            return false;
        }
        
        if (_stepper.GetActiveIndex() == 1)
        {
            await _form.Validate();
            StateHasChanged();
            return !_form.IsValid;
        }
        
        return false;
    } 
    private void CompleteRegistration()
    {
        // send code
    }

    private async void ApartmentRentalForm()
    {
        _currentFormType = FormType.APARTMENT_RENTAL;
        await MoveToNextStep();
    }

    private async void AdvertisingSpaceRentalForm()
    {
        _currentFormType = FormType.ADVERTISING_SPACE_RENTAL;
        await MoveToNextStep();
    }
    
    private async void EventOrganizationForm()
    {
        _currentFormType = FormType.EVENT_ORGANIZATION;
        await MoveToNextStep();

    } 
    
    private async void OtherForm()
    {
        _currentFormType = FormType.OTHER;
        await MoveToNextStep();
    }

    private async Task MoveToNextStep()
    {
        var currentStepIndex = _stepper.GetActiveIndex();
        
        _loading = true;
        StateHasChanged();
        
        _stepper.CompleteStep(currentStepIndex);
        _stepper.SetActiveStepByIndex(currentStepIndex + 1);
        await Task.Delay(1000);
        _loading = false;
        StateHasChanged();
    }
}