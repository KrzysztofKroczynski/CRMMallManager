using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MallManager.Components.Forms;

public class RegistrationForm : MudForm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password))]
    public string Password2 { get; set; }

    

}