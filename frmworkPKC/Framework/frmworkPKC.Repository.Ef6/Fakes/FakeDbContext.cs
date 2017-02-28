// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeDbContext.cs" company="EPAM Systems">
// Copyright 2015   
// </copyright>
// <summary>
//   Defines the IFakeDbContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The fake db context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class FakeDbContext : IFakeDbContext
    {
        #region Private Fields  
        /// <summary>
        /// The fake database sets
        /// </summary>
        private readonly Dictionary<Type, object> fakeDbSets;
        #endregion Private Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbContext" /> class.
        /// </summary>
        protected FakeDbContext()
        {
            this.fakeDbSets = new Dictionary<Type, object>();
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// Returns integer.
        /// </returns>
        public int SaveChanges()
        {
            return default(int);
        }

        /// <summary>
        /// The sync object state.
        /// </summary>
        /// <typeparam name="TEntity">The entity object which of type class and implements the <see cref="IObjectState" /></typeparam>
        /// <param name="entity">The entity.</param>
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            // no implentation needed, unit tests which uses FakeDbContext since there is no actual database for unit tests, 
            // there is no actual DbContext to sync with, please look at the Integration Tests for test that will run against an actual database.
        }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) { return new Task<int>(() => default(int)); }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<int> SaveChangesAsync() { return new Task<int>(() => default(int)); }

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
        /// <exception cref="System.NotImplementedException"></exception>
        protected void Dispose(bool disposing)
        { }

        /// <summary>
        /// The set method.
        /// </summary>
        /// <typeparam name="T">The class type</typeparam>
        /// <returns>
        /// The <see cref="DbSet" />.
        /// </returns>
        public DbSet<T> Set<T>() where T : class
        {
            return (DbSet<T>)this.fakeDbSets[typeof(T)];
        }

        /// <summary>
        /// The add fake db set.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TFakeDbSet">The type of the fake database set.</typeparam>
        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            this.fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }

        /// <summary>
        /// The sync objects state post commit.
        /// </summary>
        public void SyncObjectsStatePostCommit()
        {
        }
    }
}