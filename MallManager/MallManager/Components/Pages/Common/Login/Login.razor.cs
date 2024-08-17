using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Common.Login;

public partial class Login : ComponentBase
{
    private readonly LoginModel Model = new();


    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}