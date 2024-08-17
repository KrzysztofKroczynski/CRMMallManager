using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Tenant.RequestsPage;

public partial class RequestsPage : ComponentBase
{
    private readonly List<Request> exampleList = new()
    {
        new()
        {
            RequestNumber = "REQ001", Status = "Open", SubmissionDate = new DateTime(2021, 12, 01),
            StatusChangedDate = new DateTime(2021, 12, 05)
        },
        new()
        {
            RequestNumber = "REQ002", Status = "Closed", SubmissionDate = new DateTime(2021, 12, 02),
            StatusChangedDate = new DateTime(2021, 12, 06)
        },
        new()
        {
            RequestNumber = "REQ003", Status = "In Progress", SubmissionDate = new DateTime(2021, 12, 03),
            StatusChangedDate = new DateTime(2021, 12, 07)
        }
    };

    private void EditRequest(Request request)
    {
        // Business logic to edit request
    }

    private void DeleteRequest(Request request)
    {
        // Business logic to delete request
    }
}

internal class Request
{
    public string RequestNumber { get; set; }
    public string Status { get; set; }
    public DateTime SubmissionDate { get; set; }
    public DateTime StatusChangedDate { get; set; }
}