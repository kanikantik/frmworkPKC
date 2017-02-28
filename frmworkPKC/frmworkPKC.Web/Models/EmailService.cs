// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailService.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The EmailService Config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    /// <summary>
    ///     Email Service
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Action Result</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}