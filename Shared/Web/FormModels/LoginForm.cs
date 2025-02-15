using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Shared.Web.FormModels;

public class LoginForm : MudForm
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
}