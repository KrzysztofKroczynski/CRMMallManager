using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Shared.Web.FormModels;

public class LoginForm : MudForm
{
    [Required] [EmailAddress] public string Email = string.Empty;

    [Required] public string Password = string.Empty;
}