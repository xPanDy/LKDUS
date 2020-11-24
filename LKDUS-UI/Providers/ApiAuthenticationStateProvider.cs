using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LKDUS_UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage,
            JwtSecurityTokenHandler tokenHandler)
        {
            this.localStorage = localStorage;
            this.tokenHandler = tokenHandler;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //says if the person is authorized or not

            try
            {
                var savedToken = await this.localStorage.GetItemAsync<string>("authToken");
                if(string.IsNullOrWhiteSpace(savedToken))
                {
                    //nobody is present or nobody is home
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                var tokenContent = this.tokenHandler.ReadJwtToken(savedToken);
                var expiry = tokenContent.ValidTo;
                if(expiry < DateTime.Now)
                {
                    await this.localStorage.RemoveItemAsync("authToken");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                }

                //claims from token and build auth user object
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

                return new AuthenticationState(user);

                //return auth person
            }
            catch (Exception)
            {
                
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                 
            }
        }

        public async Task LoggedIn()
        {
            var savedToken = await this.localStorage.GetItemAsync<string>("authToken");
            var tokenContent = this.tokenHandler.ReadJwtToken(savedToken);
         //   var expiry = tokenContent.ValidTo;
            var claims = ParseClaims(tokenContent);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void LoggedOut()
        {
          
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));


            return claims;
        }
    }
}
