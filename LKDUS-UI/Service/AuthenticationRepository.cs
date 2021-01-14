using Blazored.LocalStorage;
using LKDUS_UI.Contracts;
using LKDUS_UI.Models;
using LKDUS_UI.Providers;
using LKDUS_UI.Static;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
 

namespace LKDUS_UI.Service
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IHttpClientFactory client;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationRepository(IHttpClientFactory client,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Login(LoginModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.LoginEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(user)
                , Encoding.UTF8, "application/json");

            var client = this.client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);



            if(!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);

            //Storing the content
            await this.localStorage.SetItemAsync("authToken", token.Token);


            //Change the auth state of the app

            await ((ApiAuthenticationStateProvider)this.authenticationStateProvider).LoggedIn();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            return true;// response.IsSuccessStatusCode;
        }

        public Task<IList<LoginModel>> DisplayLoginUsers()
        {
            throw new NotImplementedException();
        }


        public async Task Logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)this.authenticationStateProvider).LoggedOut();


        }

        public Task<LoginModel> Get(string url, int id)
        {
            throw new NotImplementedException();
        }

        public Task<LoginModel> Get( int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<LoginModel>> Get(string url)
        {
            try
            {
                var client = this.client.CreateClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);



                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return (IList<LoginModel>)JsonConvert.DeserializeObject<IList<LoginModel>>(content);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }
           

        }

        public Task<bool> Create(string url, LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string url, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, LoginModel obj, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(LoginModelCreate newUser)
        {
            var request = new HttpRequestMessage(HttpMethod.Put
              , Endpoints.RegisterUserEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(newUser)
                , Encoding.UTF8, "application/json");

            var client = this.client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public Task<bool> Put(LoginModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Put(string url, LoginModel obj)
        {
            throw new NotImplementedException();
        }

        Task<LoginModelCreate> IBaseRepository<LoginModelCreate>.Get(string url, int id)
        {
            throw new NotImplementedException();
        }

        Task<LoginModelCreate> IBaseRepository<LoginModelCreate>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IList<LoginModelCreate>> IBaseRepository<LoginModelCreate>.Get(string url)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(string url, LoginModelCreate obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Put(string url, LoginModelCreate obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, LoginModelCreate obj, int id)
        {
            throw new NotImplementedException();
        }
    }
}
