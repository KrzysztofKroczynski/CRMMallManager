using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MallManager.Components.Pages.Manager.GuestsRequests;

public partial class GuestsRequests : ComponentBase
{
    private readonly List<GuestRequestExampleModel> exampleList = new()
    {
        new()
        {
            RequestNumber = "REQ001", ApartmentNumber = "A101", GuestData = "John Doe", Status = "Zakończony",
            DateOfSubmission = new DateTime(2021, 01, 10), DateOfUpdate = new DateTime(2021, 01, 20)
        },
        new()
        {
            RequestNumber = "REQ002", ApartmentNumber = "A102", GuestData = "Jane Smith", Status = "Aktywny",
            DateOfSubmission = new DateTime(2021, 02, 15), DateOfUpdate = new DateTime(2021, 02, 25)
        },
        new()
        {
            RequestNumber = "REQ003", ApartmentNumber = "A103", GuestData = "Bob Brown", Status = "Zakończony",
            DateOfSubmission = new DateTime(2021, 03, 20), DateOfUpdate = new DateTime(2021, 04, 01)
        }
    };

    private EditForm? _form = new();
    private GuestRequestExampleModel? _selectedItem;

    private bool ShowCompleted;

    public IEnumerable<GuestRequestExampleModel> FilteredExampleList =>
        ShowCompleted ? exampleList : exampleList.Where(x => x.Status != "Zakończony");

    private void ToogleForm(GuestRequestExampleModel item)
    {
        _selectedItem = _selectedItem == item ? null : item;
    }

    private void OnValidSubmit(EditContext context)
    {
        // TODO: Business logic
    }

    private void AcceptRequest(GuestRequestExampleModel request)
    {
        // TODO: Business logic
    }

    private void RejectRequest(GuestRequestExampleModel request)
    {
        // TODO: Business logic
    }
}

public class GuestRequestExampleModel
{
    public string RequestNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public string GuestData { get; set; }
    public string Status { get; set; }
    public DateTime DateOfSubmission { get; set; }
    public DateTime DateOfUpdate { get; set; }
    public string? Details { get; set; }
}

public class MessageExampleModel
{
    public string Message { get; set; }
}