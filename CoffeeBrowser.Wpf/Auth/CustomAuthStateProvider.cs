using CoffeeBrowser.Library.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace CoffeeBrowser.Wpf.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider, ICustomAuthStateProvider
{
    // private ClaimsPrincipal currentUser =  new ClaimsPrincipal(new ClaimsIdentity());

    private ClaimsPrincipal currentUser = GetWindowPrincipal();

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithExternalProviderAsync();
            currentUser = user;

            return new AuthenticationState(currentUser);
        }
    }

    private Task<ClaimsPrincipal> LoginWithExternalProviderAsync()
    {
        /*  var claims = new[]
          {
              new Claim(ClaimTypes.Name, "Prem"),
              new Claim(ClaimTypes.Role, "Admin")
          };
          var identity = new ClaimsIdentity(claims, "Custom");
         var authenticatedUser = new ClaimsPrincipal(identity);
        */
        // Use for Window Authentication


        ClaimsPrincipal authenticatedUser = GetWindowPrincipal();

        return Task.FromResult(authenticatedUser);
    }

    private static ClaimsPrincipal GetWindowPrincipal()
    {
        var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity);
    }

    public void Logout()
    {
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}
