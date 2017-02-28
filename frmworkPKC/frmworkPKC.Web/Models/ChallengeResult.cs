// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The AccountController.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Owin.Security;

    /// <summary>
    /// ChallengeResult
    /// </summary>
    internal class ChallengeResult : HttpUnauthorizedResult
    {
        // Used for XSRF protection when adding external logins
        /// <summary>
        /// The XSRF key
        /// </summary>
        private const string XsrfKey = "XsrfId";

        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeResult" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeResult" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="userId">The user identifier.</param>
        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            this.LoginProvider = provider;
            this.RedirectUri = redirectUri;
            this.UserId = userId;
        }
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        /// <value>
        /// The login provider.
        /// </value>
        public string LoginProvider { get; set; }
        /// <summary>
        /// Gets or sets the redirect URI.
        /// </summary>
        /// <value>
        /// The redirect URI.
        /// </value>
        public string RedirectUri { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }
        /// <summary>
        /// Executes the result.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = this.RedirectUri };
            if (this.UserId != null)
            {
                properties.Dictionary[XsrfKey] = this.UserId;
            }
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
        }
    }
}