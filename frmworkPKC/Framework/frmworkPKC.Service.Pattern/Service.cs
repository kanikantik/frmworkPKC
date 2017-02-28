// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Service.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Service.Pattern
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using frmworkPKC.Repository.Pattern.Infrastructure;
    using frmworkPKC.Repository.Pattern.Repositories;

    /// <summary>
    /// The service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields

        /// <summary>
        /// The repository.
        /// </summary> 
        private readonly IRepositoryAsync<TEntity> repository;
        #endregion Private Fields

        #region Constructor  

        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TEntity}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public Service(IRepositoryAsync<TEntity> repository)
        {
            this.repository = repository;
        }

        #endregion Constructor

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            this.repository.Insert(entity);
        }

        /// <summary>
        /// The insert range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            this.repository.InsertRange(entities);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            this.repository.Update(entity);
        }

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            this.repository.UpdateRange(entities);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            this.repository.DeleteRange(entities);
        }

        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        public virtual void Delete(object id)
        {
            this.repository.Delete(id);
        }

        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntity entity)
        {
            this.repository.Delete(entity);
        }

        /// <summary>
        /// The queryable.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> Queryable()
        {
            return this.repository.Queryable();
        }

        /// <summary>
        /// The insert or update graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            this.repository.InsertOrUpdateGraph(entity);
        }

        /// <summary>
        /// The insert graph range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            this.repository.InsertGraphRange(entities);
        }

        /// <summary>
        /// The query method.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public IQueryFluent<TEntity> Query()
        {
            return this.repository.Query();
        }

        /// <summary>
        /// The query method.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return this.repository.Query(queryObject);
        }

        /// <summary>
        /// The query method.
        /// </summary>
        /// <param name="query">The query parameter.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return this.repository.Query(query);
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
            return await this.repository.FindAsync(keyValues);
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
            return await this.repository.FindAsync(cancellationToken, keyValues);
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
            return await this.DeleteAsync(CancellationToken.None, keyValues);
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
            return await this.repository.DeleteAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// The find method.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="TEntity" />.
        /// </returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            return this.repository.Find(keyValues);
        }

        /// <summary>
        /// The select query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The <see cref="IQueryable" />.
        /// </returns>
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return this.repository.SelectQuery(query, parameters).AsQueryable();
        }
    }
}