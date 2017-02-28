using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace frmworkPKC.Entities.Context
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using frmworkPKC.Entities.Mappings;
    using frmworkPKC.Repository.Ef6;

    /// <summary>
    /// The northwind data context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NorthwindDataContext : DataContext
    {
        /// <summary>
        /// Initializes static members of the <see cref="NorthwindDataContext" /> class.
        /// </summary>
        static NorthwindDataContext()
        {
            Database.SetInitializer<NorthwindDataContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NorthwindDataContext" /> class.
        /// </summary>
        public NorthwindDataContext()
            : base("Name=DefaultConnection")
        {
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the suppliers.
        /// </summary>
        /// <value>
        /// The suppliers.
        /// </value>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new SupplierMap());
        }
    }
}
