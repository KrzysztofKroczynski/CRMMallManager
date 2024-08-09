using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Manager.RentalsOfTenantsPage;

public partial class RentalsOfTenantsPage : ComponentBase
{
    public bool ShowCompleted { get; set; } = false;
    
    public IEnumerable<ExampleRentalModel> FilteredExampleList => ShowCompleted ? exampleList : exampleList.Where(x => x.EndDate > DateTime.Now);
    
    private List<ExampleRentalModel> exampleList = new List<ExampleRentalModel>
    {
        new ExampleRentalModel {
            RentalNumber = "RN001",
            ApartmentNumber = "A101",
            StartDate = new DateTime(2022, 01, 01),
            EndDate = new DateTime(2022, 12, 31),
            DateOfNextRentPayment = new DateTime(2022, 07, 01),
            RentAmount = 1200.00M,
            LastRevenueAmount = 1150.00M,
            NextRevenueSubmissionDate = new DateTime(2022, 07, 15)
        },
        new ExampleRentalModel {
            RentalNumber = "RN002",
            ApartmentNumber = "A102",
            StartDate = new DateTime(2022, 02, 01),
            EndDate = new DateTime(2022, 11, 30),
            DateOfNextRentPayment = new DateTime(2022, 08, 01),
            RentAmount = 1500.00M,
            LastRevenueAmount = 1450.00M,
            NextRevenueSubmissionDate = new DateTime(2022, 08, 15)
        },
        new ExampleRentalModel {
            RentalNumber = "RN003",
            ApartmentNumber = "A103",
            StartDate = new DateTime(2022, 03, 01),
            EndDate = new DateTime(2023, 03, 01),
            DateOfNextRentPayment = new DateTime(2022, 09, 01),
            RentAmount = 1300.00M,
            LastRevenueAmount = 1250.00M,
            NextRevenueSubmissionDate = new DateTime(2022, 09, 15)
        }
    };
}

public class ExampleRentalModel
{
    public string RentalNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateOfNextRentPayment { get; set; }
    public decimal RentAmount { get; set; }
    public decimal LastRevenueAmount { get; set; }
    public DateTime NextRevenueSubmissionDate { get; set; }
}
