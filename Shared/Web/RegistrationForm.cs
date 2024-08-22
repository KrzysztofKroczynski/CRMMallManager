using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Shared.Web;

public class RegistrationForm : MudForm
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8, ErrorMessage = "Password has to be at least 8 characters long.")]
    [MaxLength(30, ErrorMessage = "Password has to be shorter than 30 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Repeating password is required.")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string Password2 { get; set; } = string.Empty;
}