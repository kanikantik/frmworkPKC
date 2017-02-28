// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StateHelper.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The state helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6
{
    using System;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The state helper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class StateHelper
    {
        /// <summary>
        /// The converts the object state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>
        /// The <see cref="EntityState" />.
        /// </returns>
        public static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;

                case ObjectState.Modified:
                    return EntityState.Modified;

                case ObjectState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        /// <summary>
        /// The convert state.
        /// </summary>
        /// <param name="state">The entity state.</param>
        /// <returns>
        /// The <see cref="ObjectState" /> .
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">state message</exception>
        /// <exception cref="ArgumentOutOfRangeException">Argument Out Of Range Exception</exception>
        /// throws
        /// if the EntityState
        /// is not matching.
        public static ObjectState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                case EntityState.Unchanged:
                    return ObjectState.Unchanged;

                case EntityState.Added:
                    return ObjectState.Added;

                case EntityState.Deleted:
                    return ObjectState.Deleted;

                case EntityState.Modified:
                    return ObjectState.Modified;

                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}