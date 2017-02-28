// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactsController.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The ContactsController.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Http;
    /// <summary>
    /// Contacts Controller
    /// </summary>
    public class ContactsController : ApiController
    {
        /// <summary>
        /// MaritalStatus
        /// </summary>
        public enum MaritalStatus
        {
            /// <summary>
            /// The married
            /// </summary>
            Married,
            /// <summary>
            /// The in relation
            /// </summary>
            InRelation,
            /// <summary>
            /// The divorsed
            /// </summary>
            Divorsed,
            /// <summary>
            /// The widowed
            /// </summary>
            Widowed
        }
        // GET api/<controller>
        /// <summary>
        /// returns the Contacts
        /// </summary>
        /// <returns>
        /// contacts as a collection
        /// </returns>
        public IEnumerable<UserContact> Get()
        {
            return new UserContact[]
                       {
                           new UserContact { Id = 1, Name = "lakshman", Email = "lpeethan@xya.com" },
                           new UserContact { Id = 2, Name = "sdfsd", Email = "lpeethan@xya.com" },
                           new UserContact { Id = 3, Name = "dfsd", Email = "lpeethan@xya.com" },
                           new UserContact { Id = 4, Name = "dfsddsfsd", Email = "lpeethan@xya.com" },
                           new UserContact { Id = 5, Name = "abc", Email = "lpeethan@xya.com" },
                           new UserContact { Id = 6, Name = "def", Email = "lpeethan@xya.com" },
                       };
        }
        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user parameter.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        public bool Add(UserContact user)
        {
            return true;
        }
        /// <summary>
        /// Updates the u ser.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user parameter.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPut]
        public bool UpdateUSer(string id, UserContact user)
        {
            return true;
        }
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpDelete]
        public bool DeleteUser(string id)
        {
            return true;
        }
        /// <summary>
        /// User Class
        /// </summary>
        public class UserContact
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            [Key]
            public int Id { get; set; }
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>
            /// The name string.
            /// </value>
            [Required]
            public string Name { get; set; }
            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            /// <value>
            /// The email.
            /// </value>
            [Required]
            public string Email { get; set; }
            /// <summary>
            /// Gets or sets the age.
            /// </summary>
            /// <value>
            /// The age integer.
            /// </value>
            public int Age { get; set; }
            /// <summary>
            /// Gets or sets the date of birth.
            /// </summary>
            /// <value>
            /// The date of birth.
            /// </value>
            public DateTime DateOfBirth { get; set; }
            /// <summary>
            /// Gets or sets the addresses.
            /// </summary>
            /// <value>
            /// The addresses.
            /// </value>
            public IList<Address> Addresses { get; set; }
            /// <summary>
            /// Gets or sets the intrests.
            /// </summary>
            /// <value>
            /// The intrests.
            /// </value>
            public IList<string> Intrests { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether this instance is alive.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is alive; otherwise, <c>false</c>.
            /// </value>
            public bool IsAlive { get; set; }
            /// <summary>
            /// Gets or sets the marital status.
            /// </summary>
            /// <value>
            /// The marital status.
            /// </value>
            public MaritalStatus MaritalStatus { get; set; }
        }
        /// <summary>
        /// Address Class
        /// </summary>
        public class Address
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            [Key]
            public int Id { get; set; }
            /// <summary>
            /// Gets or sets the address line1.
            /// </summary>
            /// <value>
            /// The address line1.
            /// </value>
            [Required]
            public string AddressLine1 { get; set; }
            /// <summary>
            /// Gets or sets the address line2.
            /// </summary>
            /// <value>
            /// The address line2.
            /// </value>
            public string AddressLine2 { get; set; }
            /// <summary>
            /// Gets or sets the city.
            /// </summary>
            /// <value>
            /// The city string.
            /// </value>
            [Required]
            public string City { get; set; }
            /// <summary>
            /// Gets or sets the zip code.
            /// </summary>
            /// <value>
            /// The zip code.
            /// </value>
            [Required]
            public string ZipCode { get; set; }
        }
    }
}