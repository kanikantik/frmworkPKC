// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryObject.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   Defines the QueryObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using LinqKit;
    using frmworkPKC.Repository.Pattern.Repositories;

    /// <summary>
    /// The query object.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
    {
        /// <summary>
        /// The query expression
        /// </summary>
        private Expression<Func<TEntity, bool>> queryExpression;

        /// <summary>
        /// The query.
        /// </summary>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return this.queryExpression;
        }

        /// <summary>
        /// The and method.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        {
            return this.queryExpression = queryExpression == null ? query : queryExpression.And(query.Expand());
        }

        /// <summary>
        /// The or method.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        {
            return queryExpression = queryExpression == null ? query : queryExpression.Or(query.Expand());
        }

        /// <summary>
        /// The and method.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        public Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject)
        {
            return And(queryObject.Query());
        }

        /// <summary>
        /// The or method.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// The <see cref="Expression" />.
        /// </returns>
        public Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject)
        {
            return Or(queryObject.Query());
        }
    }
}