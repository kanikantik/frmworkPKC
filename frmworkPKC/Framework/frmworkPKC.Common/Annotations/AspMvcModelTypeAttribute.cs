// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcModelTypeAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcModelTypeAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC model type.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Controller.View(String, Object)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcModelTypeAttribute : Attribute { }
}