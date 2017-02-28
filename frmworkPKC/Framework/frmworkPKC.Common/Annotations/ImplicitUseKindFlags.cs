// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImplicitUseKindFlags.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the ImplicitUseKindFlags type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// The implicit use kind flags.
    /// </summary>
    [Flags]
    public enum ImplicitUseKindFlags
    {
        /// <summary>
        /// The default
        /// </summary>
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
        /// <summary>
        /// Only entity marked with attribute considered used
        /// </summary>
        Access = 1,
        /// <summary>
        /// Indicates implicit assignment to a member
        /// </summary>
        Assign = 2,
        /// <summary>
        /// Indicates implicit instantiation of a type with fixed constructor signature.
        /// That means any unused constructor parameters won't be reported as such.
        /// </summary>
        InstantiatedWithFixedConstructorSignature = 4,
        /// <summary>
        /// Indicates implicit instantiation of a type
        /// </summary>
        InstantiatedNoFixedConstructorSignature = 8,
    }
}