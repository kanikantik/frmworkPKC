// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryMap.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The CategoryMap.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Diagnostics.CodeAnalysis;
    using Entities;
    /// <summary>
    /// The category map.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMap" /> class.
        /// </summary>
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryId);

            // Properties
            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Categories");
            this.Property(t => t.CategoryId).HasColumnName("CategoryID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Picture).HasColumnName("Picture");
        }

    }
}
