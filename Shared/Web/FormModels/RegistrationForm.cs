﻿using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Shared.Web.FormModels;

public class RegistrationForm : MudForm
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Repeating password is required")]
    [Compare(nameof(Password), ErrorMessage = "Password does not mach")]
    public string Password2 { get; set; } = string.Empty;
}