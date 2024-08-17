using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Tenant.RegistrationPage;

public partial class ResultOfFilterApartmentsComponent : ComponentBase
{
    public List<ExampleModel> ExampleLocals = new()
    {
        new() { Id = "XXXX", Localization = "P X L X ...", StartDate = DateTime.Now, EndDate = DateTime.Now },
        new() { Id = "YYYY", Localization = "P X L Y ...", StartDate = DateTime.Now, EndDate = DateTime.Now },
        new() { Id = "ZZZZ", Localization = "P X L Z ...", StartDate = DateTime.Now, EndDate = DateTime.Now }
    };
    // TODO: Place here business logic for loading locals

    private string SelectedLocalId { get; set; }
}

public class ExampleModel
{
    public string Id { get; set; }
    public string Localization { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}