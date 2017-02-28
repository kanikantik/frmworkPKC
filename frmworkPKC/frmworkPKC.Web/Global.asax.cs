// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvcApplication.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The MvcApplication.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System;
    using System.Web.Optimization;
    using System.Web.Routing;

    /// <summary>
    ///     Mvc Application
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
