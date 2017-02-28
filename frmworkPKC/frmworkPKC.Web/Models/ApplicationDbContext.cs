// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The ApplicationDbContext.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    ///     Application Db Context
    /// </summary>
    public class ApplicationDbContext
                    : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Action Result</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}