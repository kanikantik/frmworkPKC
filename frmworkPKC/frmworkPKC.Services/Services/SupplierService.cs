﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SupplierService.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   Autogenerated Web Services
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Services
{
    using Entities;
    using Repository;
    using frmworkPKC.Service.Pattern;
	
	/// <summary>
    /// The supplier service.
    /// </summary>
    public class SupplierService : Service<Supplier>, ISupplierService
    { 
		/// <summary>
        /// The repository asynchronous
        /// </summary>
		private readonly ISupplierRepository repositoryAsync;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The async repository.
        /// </param>

        public SupplierService(ISupplierRepository repository)
            : base(repository)
        {
		    this.repositoryAsync = repository;
        }
	}
}
