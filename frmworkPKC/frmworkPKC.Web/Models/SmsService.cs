// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmsService.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The SmsService.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Models
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    /// <summary>
    ///     SmsService
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Action Result</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}