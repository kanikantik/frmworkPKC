// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryFluent.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the IQueryFluent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Pattern.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The QueryFluent interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IQueryFluent<TEntity> where TEntity : class, IObjectState
    {
        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <param name="orderBy">The order by parameter.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        /// <summary>
        /// The include.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// The <see cref="IQueryFluent" />.
        /// </returns>
        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// The select page.
        /// </summary>
        /// <param name="page">The page integer.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);

        /// <summary>
        /// The select.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        /// The select.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TEntity> Select();

        /// <summary>
        /// The select async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<IEnumerable<TEntity>> SelectAsync();

        /// <summary>
        /// The SQL query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The <see cref="IQueryable" />.
        /// </returns>
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}