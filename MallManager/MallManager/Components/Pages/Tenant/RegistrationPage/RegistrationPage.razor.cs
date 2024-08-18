using MallManager.Components.Forms.ApartmentRentalForm;
using Microsoft.AspNetCore.Components;
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
    MudStepperExtended _stepper = new();

    private ApartmentRentalForm _apartmentRentalForm;
    private RentalForm _rentalForm = new();
    
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
            switch (_currentPurposeOfTheContractType)
            { 
                case PurposeOfTheContractType.APARTMENT_RENTAL:
                    if (await _apartmentRentalForm.IsFormValid())
                    {
                        SubmitApartmentRental();
                        return false;
                    }
                    break;
                case PurposeOfTheContractType.ADVERTISING_SPACE_RENTAL:
                    // TODO: Advertising space rental form validation and submitting
                    break;
                case PurposeOfTheContractType.EVENT_ORGANIZATION:
                    // TODO: Event organization form validation and submitting
                    break;
                case PurposeOfTheContractType.OTHER:
                    // TODO: Other form validation and submitting
                    break;
            }
            StateHasChanged();
            
            return true;
        }
        
        return false;
    } 
    
    private async Task SubmitApartmentRental()
    {
        // TODO: Write business logic to save model or do with it whatever you want
        Console.WriteLine("Submitting...");
        await Task.Delay(1000);
        Console.WriteLine("Done...");
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
        _stepper.CompleteStep(currentStepIndex);
        _stepper.SetActiveStepByIndex(currentStepIndex + 1);
        await Task.Delay(1000);
        _loading = false;
        StateHasChanged();
    }
}