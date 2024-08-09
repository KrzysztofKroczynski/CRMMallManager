using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudExtensions;

namespace MallManager.Components.Pages.Tenant.RegistrationPage;

// TODO: Perhaps move this enum to a seperate file
enum PurposeOfTheContractType
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
    // private Model model = new Model(); TODO: Create a data model or DTO to represent data from a form so that you can manipulate it
    private PurposeOfTheContractType _currentPurposeOfTheContractType = PurposeOfTheContractType.OTHER;
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
            
            if (_form.IsValid)
            {
                Submit();
                return false;
            }
            
            return true;
        }
        
        return false;
    } 
    private async Task Submit()
    {
        // TODO: Write business logic to save model or do with it whatever you want
        Console.WriteLine("Submitting...");
        await Task.Delay(1000);
        Console.WriteLine("Done...");
    }

    private async void RenderApartmentRentalForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.APARTMENT_RENTAL;
        await MoveToNextStep();
    }

    private async void RenderAdvertisingSpaceRentalForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.ADVERTISING_SPACE_RENTAL;
        await MoveToNextStep();
    }
    
    private async void RenderEventOrganizationForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.EVENT_ORGANIZATION;
        await MoveToNextStep();

    } 
    
    private async void RenderOtherPurposeForm()
    {
        _currentPurposeOfTheContractType = PurposeOfTheContractType.OTHER;
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