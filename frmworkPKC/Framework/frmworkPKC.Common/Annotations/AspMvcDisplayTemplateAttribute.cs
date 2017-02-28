// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcDisplayTemplateAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcDisplayTemplateAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcDisplayTemplateAttribute : Attribute { }
}