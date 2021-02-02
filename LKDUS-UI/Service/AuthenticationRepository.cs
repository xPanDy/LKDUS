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
            //   var request2 = new HttpRequestMessage(HttpMethod.Get, url+ "/04bdf95a-2320-44ee-9b6f-2ebcebb2f02d");



                HttpResponseMessage response = await client.SendAsync(request);
              //  HttpResponseMessage response2 = await client.SendAsync(request2);

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

        public async Task<bool> Update(string url, LoginModel obj)
        {

            var request = new HttpRequestMessage(HttpMethod.Put, url);

            if (obj == null)
            {
                return false;
            }

            request.Content = new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, "applicaton/json");


            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);


            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public Task<bool> Delete(string url, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUserById(string url, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, url + id);
            var client = this.client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
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

        public async  Task<bool> Update(string url, LoginModel obj, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url + id);

            if (obj == null)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                , Encoding.UTF8, "application/json");

            var client = this.client.CreateClient();
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }

        public Task<bool> Update(string url, LoginModelCreate obj, string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, LoginModelCreate obj)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginModel> Get(string url, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+ id);
            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginModel>(content);
            }

            return null;
        }

        public async Task<LoginModel> GetById(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Endpoints.AspUsersEndpoint+ id);
            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginModel>(content);
            }

            return null;
        }

        public Task<bool> Create(string url, IList<LoginModel> list)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(string url, IList<LoginModelCreate> list)
        {
            throw new NotImplementedException();
        }
    }
}
