using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Manager.TechnicalFaultsPage;

public partial class TechnicalFaultsPage : ComponentBase
{
    private List<FailureReportExample> exampleList = new()
    {
        new FailureReportExample { TechnicalFailNumber = "001", ApartmentNumber = "100", Applicant = "John Doe", Status = "Zakończony", DateTimeOfStatusUpdate = DateTime.Now.AddDays(-2), ResponsibleSubject = "Zarząd", Description = "Przeciek w kuchni", FailValue = 1500, Restorer = "Tom Smith", Comment = "Naprawiono" },
        new FailureReportExample { TechnicalFailNumber = "002", ApartmentNumber = "101", Applicant = "Alice Johnson", Status = "Aktywny", DateTimeOfStatusUpdate = DateTime.Now, ResponsibleSubject = "Zarząd", Description = "Uszkodzenie okna", FailValue = 500, Restorer = "Steve Brown", Comment = "W trakcie oceny" },
        new FailureReportExample { TechnicalFailNumber = "003", ApartmentNumber = "102", Applicant = "Mary Smith", Status = "Zakończony", DateTimeOfStatusUpdate = DateTime.Now.AddDays(-10), ResponsibleSubject = "Administrator", Description = "Awaria prądu", FailValue = 200, Restorer = "Gary White", Comment = "Naprawiono w dniu zgłoszenia" }
    };

    private bool ShowCompleted = false;

    private IEnumerable<FailureReportExample> FilteredExampleList => ShowCompleted ? exampleList : exampleList.Where(x => x.Status != "Zakończony");
    
    private void ReportTechnicalFail()
    {
        // TODO: Business logic
    }
}

public class FailureReportExample
{
    public string TechnicalFailNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public string Applicant { get; set; }
    public string Status { get; set; }
    public DateTime DateTimeOfStatusUpdate { get; set; }
    public string ResponsibleSubject { get; set; }
    public string Description { get; set; }
    public decimal FailValue { get; set; }
    public string Restorer { get; set; }
    public string Comment { get; set; }
}