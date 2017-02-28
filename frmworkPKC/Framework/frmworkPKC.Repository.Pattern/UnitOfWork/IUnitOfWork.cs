// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the IUnitOfWork type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Pattern.UnitOfWork
{
    using System;
    using System.Data;
    using frmworkPKC.Repository.Pattern.Infrastructure;
    using frmworkPKC.Repository.Pattern.Repositories;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// Returns integer.
        /// </returns>
        int SaveChanges();

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        void Dispose(bool disposing);

        /// <summary>
        /// The repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// The <see cref="IRepository" />.
        /// </returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// The commit.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        bool Commit();

        /// <summary>
        /// The rollback.
        /// </summary>
        void Rollback();
    }
}