// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublicApiAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the PublicApiAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// This attribute is intended to mark publicly available API
    /// which should not be removed and so is treated as used
    /// </summary>
    [MeansImplicitUse]
    public sealed class PublicApiAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicApiAttribute" /> class.
        /// </summary>
        public PublicApiAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicApiAttribute" /> class.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public PublicApiAttribute([NotNull] string comment)
        {
            this.Comment = comment;
        }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [NotNull]
        public string Comment { get; private set; }
    }
}