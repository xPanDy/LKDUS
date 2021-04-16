﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API
{
    public class ApiExplorerIgnores : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerName.Equals("Pwa"))
                action.ApiExplorer.IsVisible = false;
        }
    }
}
