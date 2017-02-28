// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcViewAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcViewAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view. If applied to a method, the MVC view name is calculated implicitly
    /// from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Controller.View(Object)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewAttribute : PathReferenceAttribute { }
}