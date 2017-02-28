// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryObject.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   The QueryObject interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Pattern.Repositories
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// The QueryObject interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IQueryObject<TEntity>
    {
        /// <summary>
        /// The query.
        /// </summary>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        Expression<Func<TEntity, bool>> Query();

        /// <summary>
        /// The and expression.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// The or expression.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// The and expression.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject);

        /// <summary>
        /// The or expression.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject);
    }
}