// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RazorSectionAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the RazorSectionAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Razor attribute. Indicates that a parameter or a method is a Razor section.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, Inherited = true)]
    public sealed class RazorSectionAttribute : Attribute { }
}