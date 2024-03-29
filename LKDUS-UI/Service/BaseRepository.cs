﻿using Blazored.LocalStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LKDUS_UI.Service
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory client; // this will handle all client requests
        private readonly ILocalStorageService _localStorage;
        public BaseRepository(IHttpClientFactory client, ILocalStorageService localStorage)
        {
            this.client = client;
            this._localStorage = localStorage;

        }

        public async Task<bool> Create(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj == null)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                ,                Encoding.UTF8, "application/json");

            var client = this.client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }

            return false;
             
        }
        public async Task<T> Get(string url, int id)
        {
             

            var request = new HttpRequestMessage(HttpMethod.Get, url + id);
            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null ;

        }
        public async Task<T> Get( int id)
        {


            var request = new HttpRequestMessage(HttpMethod.Get, id.ToString());
            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null;

        }

        public async Task<bool> Delete(string url, int id)
        {
            if(id < 1)
            {
                return false;
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, url+id);
            var client = this.client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;


        }

      /*  public async Task<T> Get(string url, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + id);

            var client = this.client.CreateClient();
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }


            return null;


        }*/


        public async Task<IList<T>> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }

            return null;
        }

        public async Task<bool> Update(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            
            if(obj == null)
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

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public  async Task<bool> Update(string url, T obj, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url + id);

            if (obj == null) { 
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

        public Task<bool> Put(string url, T obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, T obj, string id)
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

        public async Task<bool> Create(string url, IList<T> list)
        {
            if (list == null)
            {
                return false;
            }

            list = list.ToList();
            var client = this.client.CreateClient();
            foreach (var el in list)
            {
                if (el == null)
                {
                    return false;
                }
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(el)
                , Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return true;
                }
            }
            
           
            

          
           
           

            return false;
        }

        public async Task<T> CreateObject(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
           
            request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                , Encoding.UTF8, "application/json");

            var client = this.client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //return true;
                var content = await response.Content.ReadAsStringAsync();
                return  JsonConvert.DeserializeObject<T>(content);
            }

            return null;

            /*
             var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = this.client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }

            return null;*/
        }

        public async Task<string>  CreateObjectAndReturnId(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                , Encoding.UTF8, "application/json");

            var client = this.client.CreateClient();
            HttpResponseMessage response =   client.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //return true;
                
                string content =   response.Content.ReadAsStringAsync().Result;
                
                 return content;
            }

            return null;
        }
    }
}
