// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Supplier.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The supplier.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Entities
{
    using global::frmworkPKC.Common.Annotations;
    using global::frmworkPKC.Repository.Ef6;

    /// <summary>
    /// The supplier.
    /// </summary>
    [Service(ServiceName = "ProductService")]
    public partial class Supplier : Entity
    {
        /// <summary>
        /// Gets or sets the supplier id.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the contact name.
        /// </summary>
        /// <value>
        /// The name of the contact.
        /// </value>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact title.
        /// </summary>
        /// <value>
        /// The contact title.
        /// </value>
        public string ContactTitle { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city string.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region string.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone string.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax string.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        /// <value>
        /// The home page.
        /// </value>
        public string Homepage { get; set; }
    }
}
