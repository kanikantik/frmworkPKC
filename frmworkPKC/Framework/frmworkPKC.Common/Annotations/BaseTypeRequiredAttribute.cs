// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTypeRequiredAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the BaseTypeRequiredAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// When applied to a target attribute, specifies a requirement for any type marked
    /// with the target attribute to implement or inherit specific type or types.
    /// </summary>
    /// <example><code>
    /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
    /// public class ComponentAttribute : Attribute { }
    /// [Component] // ComponentAttribute requires implementing IComponent interface
    /// public class MyComponent : IComponent { }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    [BaseTypeRequired(typeof(Attribute))]
    public sealed class BaseTypeRequiredAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTypeRequiredAttribute"/> class.
        /// </summary>
        /// <param name="baseType">
        /// The base type.
        /// </param>
        public BaseTypeRequiredAttribute([NotNull] Type baseType)
        {
            this.BaseType = baseType;
        }

        /// <summary>
        /// Gets the base type.
        /// </summary>
        /// <value>
        /// The type value.
        /// </value>
        [NotNull]
        public Type BaseType { get; private set; }
    }
}