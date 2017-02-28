// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The WebApiConfig
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    /// <summary>
    /// Class WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
