// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcControllerAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcControllerAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that
    /// the parameter is an MVC controller. If applied to a method,
    /// the MVC controller name is calculated implicitly from the context.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcControllerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcControllerAttribute" /> class.
        /// </summary>
        public AspMvcControllerAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcControllerAttribute" /> class.
        /// </summary>
        /// <param name="anonymousProperty">The anonymous property.</param>
        public AspMvcControllerAttribute([NotNull] string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }

        /// <summary>
        /// Gets the anonymous property.
        /// </summary>
        /// <value>
        /// The anonymous property.
        /// </value>
        [NotNull]
        public string AnonymousProperty { get; private set; }
    }
}