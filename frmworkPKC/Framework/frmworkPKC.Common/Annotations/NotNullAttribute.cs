// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotNullAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the NotNullAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Indicates that the value of the marked element could never be <c>null</c>
    /// </summary>
    /// <example>
    ///   <code>
    /// [NotNull] public object Foo() {
    /// return null; // Warning: Possible 'null' assignment
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter |
        AttributeTargets.Property | AttributeTargets.Delegate |
        AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute { }
}