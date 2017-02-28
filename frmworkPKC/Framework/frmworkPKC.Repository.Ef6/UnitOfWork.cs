// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Practices.ServiceLocation;
    using frmworkPKC.Repository.Pattern.DataContext;
    using frmworkPKC.Repository.Pattern.Infrastructure;
    using frmworkPKC.Repository.Pattern.Repositories;
    using frmworkPKC.Repository.Pattern.UnitOfWork;

    /// <summary>
    /// The unit of work.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        /// <summary>
        /// The data context asynchronous
        /// </summary>
        private IDataContextAsync dataContextAsync;
        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed;
        /// <summary>
        /// The object context
        /// </summary>
        private ObjectContext objectContext;
        /// <summary>
        /// The transaction
        /// </summary>
        private DbTransaction transaction;
        /// <summary>
        /// The repositories
        /// </summary>
        private Dictionary<string, dynamic> repositories;

        #endregion Private Fields

        #region Constuctor/Dispose

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UnitOfWork(IDataContextAsync dataContext)
        {
            this.dataContextAsync = dataContext;
            this.repositories = new Dictionary<string, dynamic>();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        /// <exception cref="DbException">The connection-level error that occurred while opening the connection.</exception>
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }
            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    if (this.objectContext != null && this.objectContext.Connection.State == ConnectionState.Open)
                    {
                        this.objectContext.Connection.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }

                if (this.dataContextAsync != null)
                {
                    this.dataContextAsync.Dispose();
                    this.dataContextAsync = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            this.disposed = true;
        }

        #endregion Constuctor/Dispose

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// Returns integer.
        /// </returns>
        public int SaveChanges()
        {
            return this.dataContextAsync.SaveChanges();
        }

        /// <summary>
        /// The repository.
        /// </summary>
        /// <typeparam name="TEntity">type TEntity should be a class implementing <see cref="IObjectState" />.</typeparam>
        /// <returns>
        /// The <see cref="IRepository" />.
        /// </returns>
        /// <exception cref="ActivationException">if there is are errors resolving the service instance.</exception>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return this.RepositoryAsync<TEntity>();
        }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<int> SaveChangesAsync()
        {
            return this.dataContextAsync.SaveChangesAsync();
        }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return this.dataContextAsync.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// The repository async.
        /// </summary>
        /// <typeparam name="TEntity">type TEntity should be a class implementing <see cref="IObjectState" />.</typeparam>
        /// <returns>
        /// The <see cref="IRepositoryAsync" />.
        /// </returns>
        /// <exception cref="ActivationException">if there is are errors resolving the service instance.</exception>
        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (this.repositories == null)
            {
                this.repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            dynamic value;
            if (this.repositories.TryGetValue(type, out value))
            {
                return (IRepositoryAsync<TEntity>)value;
            }

            var repositoryType = typeof(Repository<>);

            this.repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), dataContextAsync, this));

            return this.repositories[type];
        }

        #region Unit of Work Transactions

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            this.objectContext = ((IObjectContextAdapter)this.dataContextAsync).ObjectContext;
            if (this.objectContext.Connection.State != ConnectionState.Open)
            {
                this.objectContext.Connection.Open();
            }

            this.transaction = this.objectContext.Connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.Unspecified);
        }

        /// <summary>
        /// The commit.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool Commit()
        {
            this.transaction.Commit();
            return true;
        }

        /// <summary>
        /// The rollback.
        /// </summary>
        public void Rollback()
        {
            this.transaction.Rollback();
            this.dataContextAsync.SyncObjectsStatePostCommit();
        }

        #endregion
    }
}