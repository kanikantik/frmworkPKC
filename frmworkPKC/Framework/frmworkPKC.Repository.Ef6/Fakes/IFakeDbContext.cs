// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFakeDbContext.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the IFakeDbContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Repository.Ef6.Fakes
{
    using System.Data.Entity;

    using frmworkPKC.Repository.Pattern.DataContext;

    /// <summary>
    /// The FakeDbContext interface.
    /// </summary>
    public interface IFakeDbContext : IDataContextAsync
    {
        /// <summary>
        /// The set method interface.
        /// </summary>
        /// <typeparam name="T">T type is a class</typeparam>
        /// <returns>
        /// The <see cref="DbSet" />.
        /// </returns>
        DbSet<T> Set<T>() where T : class;

        /// <summary>
        /// The add fake db set.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TFakeDbSet">The type of the fake database set.</typeparam>
        void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new();
    }
}