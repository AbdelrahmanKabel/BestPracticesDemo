using BestPracticesDemo.UI.Blazor.Services.Base;
using Blazored.LocalStorage;
using BestPracticesDemo.UI.Blazor.Auth;
using BestPracticesDemo.UI.Blazor.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BestPracticesDemo.UI.Blazor.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.AuthenticateAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.Token);
                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(email);
                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResponse.Token);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
        {
            RegisterationRequest registerationRequest = new RegisterationRequest() { FirstName = firstName, LastName = lastName, Email = email, UserName = userName, Password = password };
            var response = await _client.RegisterAsync(registerationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
