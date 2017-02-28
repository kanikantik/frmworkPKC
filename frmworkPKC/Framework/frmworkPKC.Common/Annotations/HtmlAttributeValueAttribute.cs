// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlAttributeValueAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the HtmlAttributeValueAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// The html attribute value attribute.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Field |
        AttributeTargets.Property, Inherited = true)]
    public sealed class HtmlAttributeValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttributeValueAttribute" /> class.
        /// </summary>
        /// <param name="name">The name string.</param>
        public HtmlAttributeValueAttribute([NotNull] string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name value.
        /// </value>
        [NotNull]
        public string Name { get; private set; }
    }
}