// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="EPAM Systems">
//   Copyyright 2015
// </copyright>
// <summary>
//   The repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using LinqKit;
    using frmworkPKC.Repository.Pattern.DataContext;
    using frmworkPKC.Repository.Ef6.Fakes;
    using frmworkPKC.Repository.Pattern.Infrastructure;
    using frmworkPKC.Repository.Pattern.Repositories;
    using frmworkPKC.Repository.Pattern.UnitOfWork;

    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields
        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<TEntity> databaseSet;
        /// <summary>
        /// The unitof work
        /// </summary>
        private readonly IUnitOfWorkAsync unitofWork;
        /// <summary>
        /// The entites checked
        /// </summary>
        private HashSet<object> entitiesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph
        #endregion Private Fields
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            this.DbContext = context;
            this.unitofWork = unitOfWork;
            // Temporarily for FakeDbContext, Unit Test and Fakes
            var databaseContext = context as DbContext;
            if (databaseContext != null)
            {
                databaseSet = databaseContext.Set<TEntity>();
            }
            else
            {
                var fakeContext = context as FakeDbContext;
                if (fakeContext != null)
                {
                    this.databaseSet = fakeContext.Set<TEntity>();
                }
            }
        }
        /// <summary>
        /// gets the DbContext
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public IDataContextAsync DbContext { get; private set; }
        /// <summary>
        /// The find method.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="TEntity" />.
        /// </returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            return this.databaseSet.Find(keyValues);
        }
        /// <summary>
        /// The select query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The <see cref="IQueryable" />.
        /// </returns>
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return this.databaseSet.SqlQuery(query, parameters).AsQueryable();
        }
        /// <summary>
        /// The get repository.
        /// </summary>
        /// <typeparam name="T">T type is a class</typeparam>
        /// <returns>
        /// The <see cref="IRepository" />.
        /// </returns>
        public IRepository<T> GetRepository<T>() where T : class, IObjectState
        {
            return this.unitofWork.Repository<T>();
        }
        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added;
            this.databaseSet.Add(entity);
            this.DbContext.SaveChanges();
        }
        /// <summary>
        /// The insert range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ObjectState = ObjectState.Added;
            }
            this.databaseSet.AddRange(entities);
            this.DbContext.SaveChanges();
        }
        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            this.databaseSet.Attach(entity);
            this.DbContext.SaveChanges();
        }
        /// <summary>
        /// The update range.
        /// </summary>
        /// <param name="entity">The entities.</param>
        public virtual void UpdateRange(IEnumerable<TEntity> entity)
        {
            foreach (var en in entity)
            {
                en.ObjectState = ObjectState.Modified;
                this.databaseSet.Attach(en);
            }
            this.DbContext.SaveChanges();
        }
        /// <summary>
        /// The insert graph range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            this.databaseSet.AddRange(entities);
        }
        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        public virtual void Delete(object id)
        {
            var entity = this.databaseSet.Find(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            this.databaseSet.Remove(entity);
            this.DbContext.SaveChanges();
        }
        /// <summary>
        /// The delete range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ObjectState = ObjectState.Deleted;
            }
            this.databaseSet.RemoveRange(entities);
            this.DbContext.SaveChanges();
        }
        /// <summary>
        /// The query method.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }
        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }
        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="query">The query parameter.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }
        /// <summary>
        /// The queryable.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> Queryable()
        {
            return this.databaseSet;
        }
        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await databaseSet.FindAsync(keyValues);
        }
        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await databaseSet.FindAsync(cancellationToken, keyValues);
        }
        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }
        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);
            if (entity == null)
            {
                return false;
            }
            entity.ObjectState = ObjectState.Deleted;
            databaseSet.Attach(entity);
            return true;
        }
        /// <summary>
        /// Selects the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <param name="page">The page numbers.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The <see cref="IQueryable" />.</returns>
        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = databaseSet;
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }
        /// <summary>
        /// Selects the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <param name="page">The page size.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The Task of IEnumerable.</returns>
        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }
        /// <summary>
        /// The insert or update graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            entitiesChecked = null;
            databaseSet.Attach(entity);
        }
        /// <summary>
        /// Synchronizes the object graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void SyncObjectGraph(object entity) // scan object graph for all 
        {
            if (entitiesChecked == null)
            {
                entitiesChecked = new HashSet<object>();
            }
            if (entitiesChecked.Contains(entity))
            {
                return;
            }
            entitiesChecked.Add(entity);
            var objectState = entity as IObjectState;
            if (objectState != null && objectState.ObjectState == ObjectState.Added)
            {
                this.DbContext.SyncObjectState(objectState);
            }
            // Set tracking state for child collections
            foreach (var prop in entity.GetType().GetProperties())
            {
                // Apply changes to 1-1 and M-1 properties
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if (trackableRef.ObjectState == ObjectState.Added)
                    {
                        this.DbContext.SyncObjectState(objectState);
                    }
                    SyncObjectGraph(prop.GetValue(entity, null));
                }
                // Apply changes to 1-M properties
                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null)
                {
                    continue;
                }
                Debug.WriteLine("Checking collection: " + prop.Name);
                foreach (var item in items)
                {
                    SyncObjectGraph(item);
                }
            }
        }
    }
}