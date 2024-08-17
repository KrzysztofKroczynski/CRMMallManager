using System.ComponentModel.DataAnnotations;
using MudBlazor;
using Shared.Core.Entities;

namespace Shared.Web.FormModels;

public class RentalForm : MudForm
{
    [Required(ErrorMessage = "Metraż jest wymagany")]
    public SurfaceClassDict SurfaceClassDict { get; set; }
    
    public RetailUnitPurpose RetailUnitPurpose { get; set; }
    
    [Required(ErrorMessage = "Data \"od\" jest wymagana")]
    public DateTime? StartDate { get; set; }
    
    [Required(ErrorMessage = "Data \"do\" jest wymagana")]
    public DateTime? EndDate { get; set; }
    
    [Required(ErrorMessage = "Opis jest wymagany")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Maksymalnie może zawierać 1000 znaków")]
    public string BusinessDescription { get; set; }

    [Required(ErrorMessage = "Lokal jest wymagany")]
    public RetailUnit SelectedRetailUnit { get; set; }
}