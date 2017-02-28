// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcMasterAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcMasterAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC Master.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Controller.View(String, String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcMasterAttribute : Attribute { }
}