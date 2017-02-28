// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlElementAttributesAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the HtmlElementAttributesAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// The html element attributes attribute.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Field, Inherited = true)]
    public sealed class HtmlElementAttributesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementAttributesAttribute" /> class.
        /// </summary>
        public HtmlElementAttributesAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementAttributesAttribute" /> class.
        /// </summary>
        /// <param name="name">The name parameter.</param>
        public HtmlElementAttributesAttribute([NotNull] string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name string.
        /// </value>
        [NotNull]
        public string Name { get; private set; }
    }
}