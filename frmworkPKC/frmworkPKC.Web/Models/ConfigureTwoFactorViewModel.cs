// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureTwoFactorViewModel.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The VerifyPhoneNumberViewModel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    ///     ConfigureTwoFactorViewModel
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// Gets or sets the selected provider.
        /// </summary>
        /// <value>
        /// The selected provider.
        /// </value>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public ICollection<SelectListItem> Providers { get; set; }
    }
}