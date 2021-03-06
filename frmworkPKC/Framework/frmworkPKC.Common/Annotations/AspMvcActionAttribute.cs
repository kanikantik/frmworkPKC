// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspMvcActionAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the AspMvcActionAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC action. If applied to a method, the MVC action name is calculated
    /// implicitly from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcActionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcActionAttribute"/> class.
        /// </summary>
        public AspMvcActionAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcActionAttribute"/> class.
        /// </summary>
        /// <param name="anonymousProperty">
        /// The anonymous property.
        /// </param>
        public AspMvcActionAttribute([NotNull] string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }

        /// <summary>
        /// Gets the anonymous property.
        /// </summary>
        /// <value>
        /// Returns string value
        /// </value>
        [NotNull]
        public string AnonymousProperty { get; private set; }
    }
}