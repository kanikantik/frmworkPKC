// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerifyCodeViewModel.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The VerifyCodeViewModel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Verify Code View Model
    /// </summary>
    public class VerifyCodeViewModel
    {
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        ///The code value string
        /// </value>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember browser].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember browser]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        public bool RememberMe { get; set; }
    }
}