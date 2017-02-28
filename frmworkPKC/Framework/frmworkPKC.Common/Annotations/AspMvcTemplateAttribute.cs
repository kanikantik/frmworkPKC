// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcTemplateAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcTemplateAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcTemplateAttribute : Attribute { }
}