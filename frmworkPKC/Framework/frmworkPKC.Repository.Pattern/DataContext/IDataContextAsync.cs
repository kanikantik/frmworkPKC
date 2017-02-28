// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataContextAsync.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The DataContextAsync interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Pattern.DataContext
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The DataContextAsync interface.
    /// </summary>
    public interface IDataContextAsync : IDataContext
    {
        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<int> SaveChangesAsync();
    }
}