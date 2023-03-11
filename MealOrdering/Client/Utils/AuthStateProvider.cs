using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MealOrdering.Client.Utils
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient client;
        private readonly AuthenticationState anonymus;

        public AuthStateProvider(ILocalStorageService localStorageService, HttpClient client)
        {
            this.localStorageService = localStorageService;
            this.client = client;
            anonymus = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string apiToken = await localStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrEmpty(apiToken))
                return anonymus;

            string email = await localStorageService.GetItemAsStringAsync("email");
            var claimsPrincipel = new ClaimsPrincipal(
                                  new ClaimsIdentity(
                                  new[]{ new Claim(
                                  ClaimTypes.Email, email) }, "jwtAuthType"));

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
            return new AuthenticationState(claimsPrincipel);
        }

        public void NotifyUserLogin(string email)
        {
            var claimsPrincipel = new ClaimsPrincipal(
                                  new ClaimsIdentity(
                                  new[]{ new Claim(
                                  ClaimTypes.Email, email) }, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(claimsPrincipel));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymus);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
