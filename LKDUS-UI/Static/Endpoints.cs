﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Static
{
    public static class Endpoints
    {
        //url for api ttps://localhost:44321
        public static string BaseUrl = "http://localhost:5000/";

        public static string UsersEndpoint = $"{BaseUrl}api/users/";
        public static string MeasurementEndpoint = $"{BaseUrl}api/measurements/";
        // public static string LoginEntpoint = $"{BaseUrl}api/register/"
        public static string LoginEndpoint = $"{BaseUrl}api/login/";

        public static string GetUsersEndpoint = $"{BaseUrl}api/login/";
        public static string MeasurementPositions = $"{BaseUrl}api/measurementposition/";

        public static string MeasurementTypeEndpoint = $"{BaseUrl}api/measurementtype/";
    
        
        public static string MachinesEndpoint = $"{BaseUrl}api/machine/";
        public static string AspUsersEndpoint = $"{BaseUrl}api/AspNetUsers/";
        
        public static string FusPackEndPoint = $"{BaseUrl}api/FusPack/";
       
        public static string RegisterUserEndpoint = $"{BaseUrl}api/AspNetUsers/";


    }
}
