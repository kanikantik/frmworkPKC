// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageLoginsViewModel.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The ManageLoginsViewModel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    /// <summary>
    /// ManageLoginsViewModel
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// Gets or sets the current logins.
        /// </summary>
        /// <value>
        /// The current logins.
        /// </value>
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        /// <summary>
        /// Gets or sets the other logins.
        /// </summary>
        /// <value>
        /// The other logins.
        /// </value>
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}