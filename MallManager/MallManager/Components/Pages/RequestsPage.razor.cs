using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages;

public partial class RequestsPage : ComponentBase
{
    private List<Request> exampleList = new List<Request>
    {
        new Request { RequestNumber = "REQ001", Status = "Open", SubmissionDate = new DateTime(2021, 12, 01), StatusChangedDate = new DateTime(2021, 12, 05) },
        new Request { RequestNumber = "REQ002", Status = "Closed", SubmissionDate = new DateTime(2021, 12, 02), StatusChangedDate = new DateTime(2021, 12, 06) },
        new Request { RequestNumber = "REQ003", Status = "In Progress", SubmissionDate = new DateTime(2021, 12, 03), StatusChangedDate = new DateTime(2021, 12, 07) }
    };

    private void EditRequest(Request request)
    {
        // Business logic to edit request
    }

    private void DeleteRequest(Request request)
    {
        // Business logic to delete request
    }
    
    private void NavigateToRequest()
    {
        NavigationManager.NavigateTo("/request");
    }
}

class Request
{
    public string RequestNumber { get; set; }
    public string Status { get; set; }
    public DateTime SubmissionDate { get; set; }
    public DateTime StatusChangedDate { get; set; }
}
