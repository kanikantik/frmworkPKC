// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImplicitUseTarget.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the ImplicitUseTarget type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Specify what is considered used implicitly
    /// when marked with <see cref="MeansImplicitUseAttribute" />
    /// or <see cref="UsedImplicitlyAttribute" />
    /// </summary>
    [Flags]
    public enum ImplicitUseTarget
    {
        /// <summary>
        /// The default
        /// </summary>
        Default = Itself,
        /// <summary>
        /// The itself
        /// </summary>
        Itself = 1,
        /// <summary>
        /// Members of entity marked with attribute are considered used
        /// </summary>
        Members = 2,
        /// <summary>
        /// Entity marked with attribute and all its members considered used
        /// </summary>
        WithMembers = Itself | Members
    }
}