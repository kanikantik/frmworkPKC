// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageController.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The ManageController.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Models;
    /// <summary>
    /// Manage Controller
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Authorize]
    public class ManageController : Controller
    {
        // Used for XSRF protection when adding external logins
        /// <summary>
        /// The XSRF key
        /// </summary>
        private const string XsrfKey = "XsrfId";
        /// <summary>
        /// The _sign in manager
        /// </summary>
        private ApplicationSignInManager signInManager;
        /// <summary>
        /// The _user manager
        /// </summary>
        private ApplicationUserManager userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageController" /> class.
        /// </summary>
        public ManageController()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }
        /// <summary>
        /// Manage Message Id
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// The add phone success
            /// </summary>
            AddPhoneSuccess,
            /// <summary>
            /// The change password success
            /// </summary>
            ChangePasswordSuccess,
            /// <summary>
            /// The set two factor success
            /// </summary>
            SetTwoFactorSuccess,
            /// <summary>
            /// The set password success
            /// </summary>
            SetPasswordSuccess,
            /// <summary>
            /// The remove login success
            /// </summary>
            RemoveLoginSuccess,
            /// <summary>
            /// The remove phone success
            /// </summary>
            RemovePhoneSuccess,
            /// <summary>
            /// The error value 
            /// </summary>
            Error
        }
        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        /// <value>
        /// The sign in manager.
        /// </value>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                this.signInManager = value;
            }
        }
        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }

        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>
        /// The authentication manager.
        /// </value>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        /// <summary>
        /// Indexes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : string.Empty;
            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = this.HasPassword(),
                PhoneNumber = await this.UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await this.UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await this.UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return this.View(model);
        }
        /// <summary>
        /// Removes the login.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await this.UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return this.RedirectToAction("ManageLogins", new { Message = message });
        }
        // GET: /Manage/AddPhoneNumber        
        /// <summary>
        /// Adds the phone number.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        public ActionResult AddPhoneNumber()
        {
            return this.View();
        }
        // POST: /Manage/AddPhoneNumber        
        /// <summary>
        /// Adds the phone number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            // Generate the token and send it
            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (this.UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await this.UserManager.SmsService.SendAsync(message).ConfigureAwait(false);
            }
            return this.RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }
        // POST: /Manage/EnableTwoFactorAuthentication        
        /// <summary>
        /// Enables the two factor authentication.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
            }
            return this.RedirectToAction("Index", "Manage");
        }
        // POST: /Manage/DisableTwoFactorAuthentication        
        /// <summary>
        /// Disables the two factor authentication.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
            }
            return this.RedirectToAction("Index", "Manage");
        }
        // GET: /Manage/VerifyPhoneNumber        
        /// <summary>
        /// Verifies the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber).ConfigureAwait(false);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? this.View("Error") : this.View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber, Code = code });
        }
        // POST: /Manage/VerifyPhoneNumber        
        /// <summary>
        /// Verifies the phone number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            var result = await this.UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Failed to verify phone");
            return this.View(model);
        }
        // GET: /Manage/RemovePhoneNumber        
        /// <summary>
        /// Removes the phone number.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await this.UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return this.RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
            }
            return this.RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }
        // GET: /Manage/ChangePassword        
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        public ActionResult ChangePassword()
        {
            return this.View();
        }
        // POST: /Manage/ChangePassword        
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            var result = await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            this.AddErrors(result);
            return this.View(model);
        }
        // GET: /Manage/SetPassword        
        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        public ActionResult SetPassword()
        {
            return this.View();
        }
        /// <summary>
        /// The set password. POST: /Manage/SetPassword
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                    }
                    return this.RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                this.AddErrors(result);
            }
            return this.View(model);
        }
        /// <summary>
        /// The manage logins. GET: /Manage/ManageLogins
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;
            var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return this.View("Error");
            }
            var userLogins = await this.UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = this.AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            this.ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }
        /// <summary>
        /// The link login. POST: /Manage/LinkLogin
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            return new ChallengeResult(provider, this.Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }
        /// <summary>
        /// The link login callback. GET: /Manage/LinkLoginCallback
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await this.UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? this.RedirectToAction("ManageLogins") : this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }
        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }
        #region Helpers
        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
        /// <summary>
        /// Determines whether this instance has password.
        /// </summary>
        /// <returns>Returns Boolean Value</returns>
        private bool HasPassword()
        {
            var user = this.UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        /// <summary>
        /// Determines whether [has phone number].
        /// </summary>
        /// <returns>Returns Boolean Value</returns>
        private bool HasPhoneNumber()
        {
            var user = this.UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }
        #endregion 
    }
}