﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Static
{
    public static class Endpoints
    {
        //url for api                   https://localhost:44321
        public static string BaseUrl = "https://localhost:44321/";

        public static string UsersEndpoint = $"{BaseUrl}api/users/";
        public static string BooksEndpoint = $"{BaseUrl}api/measurements/";
        // public static string LoginEntpoint = $"{BaseUrl}api/register/"
        public static string LoginEndpoint = $"{BaseUrl}api/login/";

        public static string GetUsersEndpoint = $"{BaseUrl}api/login/";
    }
}
