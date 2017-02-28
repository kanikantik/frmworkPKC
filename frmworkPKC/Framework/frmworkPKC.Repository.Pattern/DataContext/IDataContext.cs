// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataContext.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The DataContext interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Pattern.DataContext
{
    using System;
    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The DataContext interface.
    /// </summary>
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// Returns integer.
        /// </returns>
        int SaveChanges();

        /// <summary>
        /// The sync object state.
        /// </summary>
        /// <typeparam name="TEntity">The entity object which of type class and implements the <see cref="IObjectState" /></typeparam>
        /// <param name="entity">The entity.</param>
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;

        /// <summary>
        /// The sync objects state post commit.
        /// </summary>
        void SyncObjectsStatePostCommit();
    }
}