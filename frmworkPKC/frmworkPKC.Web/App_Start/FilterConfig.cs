// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The FilterConfig.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web
{
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class FilterConfig.
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
