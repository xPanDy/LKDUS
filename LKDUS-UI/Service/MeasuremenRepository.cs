﻿using Blazored.LocalStorage;
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
    public class MeasurementRepository : BaseRepository<Measurement>, IMeasurementRepository
    {

        private readonly IHttpClientFactory clientFactory;
        private readonly ILocalStorageService localStorageService;

        public MeasurementRepository(IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService) : base(httpClientFactory, localStorageService)
        {
            this.clientFactory = httpClientFactory;
            this.localStorageService = localStorageService;
        }
    }

}