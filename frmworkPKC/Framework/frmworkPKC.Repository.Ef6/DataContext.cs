// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataContext.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   Defines the DataContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using frmworkPKC.Repository.Pattern.DataContext;
    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The data context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DataContext : DbContext, IDataContextAsync
    {
        #region Private Fields
        /// <summary>
        /// The instance identifier
        /// </summary>
        private readonly Guid instanceId;
        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed;
        #endregion Private Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext" /> class.
        /// </summary>
        public DataContext()
            : base("DefaultConnection")
        {
            this.instanceId = Guid.NewGuid();
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext" /> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.instanceId = Guid.NewGuid();
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Gets the instance id.
        /// </summary>
        /// <value>
        /// The instance identifier.
        /// </value>
        public Guid InstanceId
        {
            get
            {
                return this.instanceId;
            }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">A database command did not affect the expected number of rows. This usually
        /// indicates an optimistic concurrency violation; that is, a row has been changed
        /// in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">An attempt was made to use unsupported behavior such as executing multiple
        /// asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">Some error occurred attempting to process entities in the context either
        /// before or after sending commands to the database.</exception>
        /// <seealso cref="DbContext.SaveChanges" />
        public override int SaveChanges()
        {
            this.SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            this.SyncObjectsStatePostCommit();
            return changes;
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous save operation.  The
        /// <see cref="Task.Result">Task.Result</see> contains the number of
        /// objects written to the underlying database.
        /// </returns>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">A database command did not affect the expected number of rows. This usually
        /// indicates an optimistic concurrency violation; that is, a row has been changed
        /// in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">An attempt was made to use unsupported behavior such as executing multiple
        /// asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">Some error occurred attempting to process entities in the context either
        /// before or after sending commands to the database.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <seealso cref="DbContext.SaveChangesAsync" />
        public override async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }
        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation.  The
        /// <see cref="Task.Result">Task.Result</see> contains the number of
        /// objects written to the underlying database.
        /// </returns>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">A database command did not affect the expected number of rows. This usually
        /// indicates an optimistic concurrency violation; that is, a row has been changed
        /// in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">An attempt was made to use unsupported behavior such as executing multiple
        /// asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">Some error occurred attempting to process entities in the context either
        /// before or after sending commands to the database.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <seealso cref="DbContext.SaveChangesAsync" />
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            this.SyncObjectsStatePostCommit();
            return changesAsync;
        }

        /// <summary>
        /// The sync object state.
        /// </summary>
        /// <typeparam name="TEntity">The entity object which of type class and implements the <see cref="IObjectState" /></typeparam>
        /// <param name="entity">The entity.</param>
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            this.Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        /// <summary>
        /// Synchronizes the objects state pre commit.
        /// </summary>
        private void SyncObjectsStatePreCommit()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                entityEntry.State = StateHelper.ConvertState(((IObjectState)entityEntry.Entity).ObjectState);
            }
        }

        /// <summary>
        /// The sync objects state post commit.
        /// </summary>
        public void SyncObjectsStatePostCommit()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)entityEntry.Entity).ObjectState = StateHelper.ConvertState(entityEntry.State);
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // free other managed objects that implement
                    // IDisposable only
                }

                // release any unmanaged objects
                // set object references to null
                this.disposed = true;
            }

            base.Dispose(disposing);
        }

    }
}