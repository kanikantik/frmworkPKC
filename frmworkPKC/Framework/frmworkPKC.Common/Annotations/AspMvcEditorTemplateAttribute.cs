// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcEditorTemplateAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcEditorTemplateAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcEditorTemplateAttribute : Attribute { }
}