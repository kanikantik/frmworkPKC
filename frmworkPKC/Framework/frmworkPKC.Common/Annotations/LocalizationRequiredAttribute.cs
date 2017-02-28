// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationRequiredAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the LocalizationRequiredAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Indicates that marked element should be localized or not
    /// </summary>
    /// <example>
    ///   <code>
    /// [LocalizationRequiredAttribute(true)]
    /// public class Foo {
    /// private string str = "my string"; // Warning: Localizable string
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class LocalizationRequiredAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationRequiredAttribute" /> class.
        /// </summary>
        public LocalizationRequiredAttribute() : this(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationRequiredAttribute" /> class.
        /// </summary>
        /// <param name="required">The required.</param>
        public LocalizationRequiredAttribute(bool required)
        {
            this.Required = required;
        }

        /// <summary>
        /// Gets a value indicating whether required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; private set; }
    }
}