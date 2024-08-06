using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace MallManager.Components.Forms;

public class PersonalForm : MudForm
{
    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Second Name must be at least 4 characters long.", MinimumLength = 4)]
    public string SecondName { get; set; }

    [Required]
    public string Surname { get; set; }
    
    [Required]
    public int PhoneNumber { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Country { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string BuildingNumber { get; set; }

    [Required]
    public string LocalNumber { get; set; }

    [Required]
    public string PostalCode { get; set; }

    public string AdditionalAddressInfo { get; set; }

    [Required]
    public string NIP { get; set; }

    [Required]
    public string KRS { get; set; }

    [Required]
    public string CompanySize { get; set; }

    [Required]
    public decimal InitialCapital { get; set; }
}