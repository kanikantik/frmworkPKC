// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryFluent.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   Defines the QueryFluent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using frmworkPKC.Repository.Pattern.Infrastructure;
    using frmworkPKC.Repository.Pattern.Repositories;

    /// <summary>
    /// The query fluent.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public sealed class QueryFluent<TEntity> : IQueryFluent<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields
        /// <summary>
        /// The expre ssion
        /// </summary>
        private readonly Expression<Func<TEntity, bool>> expreSsion;
        /// <summary>
        /// The includes
        /// </summary>
        private readonly List<Expression<Func<TEntity, object>>> includes;
        /// <summary>
        /// The repository
        /// </summary>
        private readonly Repository<TEntity> repository;
        /// <summary>
        /// The orderby
        /// </summary>
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby;
        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFluent{TEntity}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public QueryFluent(Repository<TEntity> repository)
        {
            this.repository = repository;
            this.includes = new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFluent{TEntity}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="queryObject">The query object.</param>
        public QueryFluent(Repository<TEntity> repository, IQueryObject<TEntity> queryObject)
            : this(repository)
        {
            expreSsion = queryObject.Query();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFluent{TEntity}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="expression">The expression.</param>
        public QueryFluent(Repository<TEntity> repository, Expression<Func<TEntity, bool>> expression)
            : this(repository)
        {
            this.expreSsion = expression;
        }

        #endregion Constructors

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="orderBy">The order by parameter.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            this.orderby = orderBy;
            return this;
        }

        /// <summary>
        /// The include.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        public IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            this.includes.Add(expression);
            return this;
        }

        /// <summary>
        /// The select page.
        /// </summary>
        /// <param name="page">The page integer.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount)
        {
            totalCount = this.repository.Select(expreSsion).Count();
            return this.repository.Select(expreSsion, orderby, includes, page, pageSize);
        }

        /// <summary>
        /// The select.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<TEntity> Select()
        {
            return this.repository.Select(this.expreSsion, this.orderby, this.includes);
        }

        /// <summary>
        /// The select.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return this.repository.Select(expreSsion, orderby, includes).Select(selector);
        }

        /// <summary>
        /// The select async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<IEnumerable<TEntity>> SelectAsync()
        {
            return await this.repository.SelectAsync(this.expreSsion, this.orderby, this.includes);
        }

        /// <summary>
        /// The SQL query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return this.repository.SelectQuery(query, parameters).AsQueryable();
        }
    }
}