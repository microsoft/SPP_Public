// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TPP.Api
{
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace; // e.g. "Controllers.V2"
            var apiVersion = controllerNamespace.Split('.').Last().ToLower();

            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}