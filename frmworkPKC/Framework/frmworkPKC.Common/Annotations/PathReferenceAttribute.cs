// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathReferenceAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the PathReferenceAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Indicates that a parameter is a path to a file or a folder
    /// within a web project. Path can be relative or absolute,
    /// starting from web root (~)
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PathReferenceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathReferenceAttribute" /> class.
        /// </summary>
        public PathReferenceAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathReferenceAttribute" /> class.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        public PathReferenceAttribute([PathReference] string basePath)
        {
            this.BasePath = basePath;
        }

        /// <summary>
        /// Gets the base path.
        /// </summary>
        /// <value>
        /// The base path.
        /// </value>
        [NotNull]
        public string BasePath { get; private set; }
    }
}