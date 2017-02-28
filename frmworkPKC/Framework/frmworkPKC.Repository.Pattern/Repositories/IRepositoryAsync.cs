// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryAsync.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   Defines the IRepositoryAsync type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Pattern.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The RepositoryAsync interface.
    /// </summary>
    /// <typeparam name="TEntity">TEntity should be a class inheriting from IObjectState</typeparam>
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<bool> DeleteAsync(params object[] keyValues);

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
