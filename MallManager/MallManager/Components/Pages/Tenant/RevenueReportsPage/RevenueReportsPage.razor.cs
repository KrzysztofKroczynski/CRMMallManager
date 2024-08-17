using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Tenant.RevenueReportsPage;

public partial class RevenueReportsPage : ComponentBase
{
    private readonly List<ExampleModel> exampleList = new()
    {
        new()
        {
            ReportNumber = "RN001",
            ApartmentNumber = "A101",
            StartDate = new DateTime(2021, 1, 1),
            EndDate = new DateTime(2021, 1, 31),
            DateOfSubmission = new DateTime(2021, 2, 1),
            Revenue = 1200.50,
            NextRevenueSubmissionDate = new DateTime(2021, 2, 28),
            WasSubmittedAfterEndDate = false
        },
        new()
        {
            ReportNumber = "RN002",
            ApartmentNumber = "A102",
            StartDate = new DateTime(2021, 2, 1),
            EndDate = new DateTime(2021, 2, 28),
            DateOfSubmission = new DateTime(2021, 3, 1),
            Revenue = 1500.00,
            NextRevenueSubmissionDate = new DateTime(2021, 3, 31),
            WasSubmittedAfterEndDate = true
        },
        new()
        {
            ReportNumber = "RN003",
            ApartmentNumber = "A103",
            StartDate = new DateTime(2021, 3, 1),
            EndDate = new DateTime(2021, 3, 31),
            DateOfSubmission = new DateTime(),
            Revenue = 1800.75,
            NextRevenueSubmissionDate = new DateTime(2021, 4, 30),
            WasSubmittedAfterEndDate = false
        }
    };

    public string? SearchCriteria { get; set; }

    private string ChangeRowBackgroundColor(ExampleModel item, int index)
    {
        if (item.DateOfSubmission == default) return "background-color: #bbc3d4";

        return item.WasSubmittedAfterEndDate ? "background-color: #f26868" : "background-color: #6ff772";
        ;
    }
}

internal class ExampleModel
{
    public string ReportNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateOfSubmission { get; set; }
    public double Revenue { get; set; }
    public DateTime NextRevenueSubmissionDate { get; set; }
    public bool WasSubmittedAfterEndDate { get; set; }
}