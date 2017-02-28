// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeDbSet.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The fake db set.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Repository.Ef6.Fakes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using frmworkPKC.Repository.Pattern.Infrastructure;

    /// <summary>
    /// The fake db set.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public abstract class FakeDbSet<TEntity> : DbSet<TEntity>, IDbSet<TEntity> where TEntity : Entity, new()
    {
        #region Private Fields
        /// <summary>
        /// The Observable Collection
        /// </summary>
        private readonly ObservableCollection<TEntity> items;
        /// <summary>
        /// The query property
        /// </summary>
        private readonly IQueryable query;
        #endregion Private Fields
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbSet{TEntity}" /> class.
        /// </summary>
        protected FakeDbSet()
        {
            this.items = new ObservableCollection<TEntity>();
            this.query = this.items.AsQueryable();
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// /// <value>
        /// The Expression value
        /// </value>
        public Expression Expression
        {
            get
            {
                return this.query.Expression;
            }
        }

        /// <summary>
        /// Gets the element type.
        /// </summary>
        /// /// <value>
        /// The type value
        /// </value>
        public Type ElementType
        {
            get
            {
                return this.query.ElementType;
            }
        }

        /// <summary>
        /// Gets the local.
        /// </summary>
        /// <value>
        /// The local.
        /// </value>
        public override ObservableCollection<TEntity> Local
        {
            get
            {
                return this.items;
            }
        }
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// /// <value>
        /// The query provider value
        /// </value>
        public IQueryProvider Provider
        {
            get
            {
                return this.query.Provider;
            }
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator" />.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator" />.
        /// </returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// The add method.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The <see cref="TEntity" />.
        /// </returns>
        public override TEntity Add(TEntity entity)
        {
            this.items.Add(entity);
            return entity;
        }
        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The <see cref="TEntity" />.
        /// </returns>
        public override TEntity Remove(TEntity entity)
        {
            this.items.Remove(entity);
            return entity;
        }
        /// <summary>
        /// The attach.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The <see cref="TEntity" />.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override TEntity Attach(TEntity entity)
        {
            switch (entity.ObjectState)
            {
                case ObjectState.Modified:
                    this.items.Remove(entity);
                    this.items.Add(entity);
                    break;

                case ObjectState.Deleted:
                    this.items.Remove(entity);
                    break;

                case ObjectState.Unchanged:
                case ObjectState.Added:
                    this.items.Add(entity);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("entity");
            }
            return entity;
        }
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="TEntity" />.
        /// </returns>
        public override TEntity Create()
        {
            return new TEntity();
        }
        /// <summary>
        /// The create.
        /// </summary>
        /// <typeparam name="TDerivedEntity">The type of the derived entity.</typeparam>
        /// <returns>
        /// The <see cref="TDerivedEntity" />.
        /// </returns>
        /// <exception cref="MissingMethodException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.The type that is specified for <paramref name="T" /> does not have a parameterless constructor.</exception>
        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }
}