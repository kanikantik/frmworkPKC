// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="EPAM Systems">
//   Copyright 2015
// </copyright>
// <summary>
//   The AccountController.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Web.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using frmworkPKC.Web.Models;

    /// <summary>
    /// Account Controller
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Authorize]
    public class AccountController : Controller
    {
        // Used for XSRF protection when adding external logins
        /// <summary>
        /// The XSRF key
        /// </summary>
        private const string XsrfKey = "XsrfId";
        /// <summary>
        /// The application sign in manager
        /// </summary>
        private ApplicationSignInManager applicationSignInManager;
        /// <summary>
        /// The application user manager
        /// </summary>
        private ApplicationUserManager applicationUserManager;

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
                return this.applicationSignInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                this.applicationSignInManager = value;
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
                return this.applicationUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.applicationUserManager = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        public AccountController()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
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
        /// Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }
        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            var status = await this.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (status)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
            }
        }
        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await this.SignInManager.HasBeenVerifiedAsync())
            {
                return this.View("Error");
            }
            return this.View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }
        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await this.SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError(string.Empty, "Invalid code.");
                    return this.View(model);
            }
        }
        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }
        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await this.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //string code = await this.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await this.UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>").ConfigureAwait(false);

                    return this.RedirectToAction("Index", "Home");
                }
                this.AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code string.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return this.View("Error");
            }
            var result = await this.UserManager.ConfirmEmailAsync(userId, code);
            return this.View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return this.View();
        }
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The ForgotPasswordViewModel type model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.UserManager.FindByNameAsync(model.Email);
                if (user == null || !await this.UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await this.UserManager.GeneratePasswordResetTokenAsync(user.Id).ConfigureAwait(false);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await this.UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>").ConfigureAwait(false);
                return this.RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        /// <summary>
        /// Forgots the password confirmation.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="code">The string code.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? this.View("Error") : this.View();
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            var user = await this.UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await this.UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            this.AddErrors(result);
            return this.View();
        }
        // GET: /Account/ResetPasswordConfirmation        
        /// <summary>
        /// Resets the password confirmation.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }
        // POST: /Account/ExternalLogin        
        /// <summary>
        /// Externals the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        // GET: /Account/SendCode        
        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await this.SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return this.View("Error");
            }
            var userFactors = await this.UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return this.View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }
        // POST: /Account/SendCode        
        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            // Generate the token and send it
            if (!await this.SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return this.View("Error");
            }
            return this.RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }
        // GET: /Account/ExternalLoginCallback        
        /// <summary>
        /// Externals the login callback.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return this.RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await this.SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return this.View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }
        // POST: /Account/ExternalLoginConfirmation        
        /// <summary>
        /// Externals the login confirmation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this.AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return this.View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await this.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                        return this.RedirectToLocal(returnUrl);
                    }
                }
                this.AddErrors(result);
            }
            ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }
        // POST: /Account/LogOff        
        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut();
            return this.RedirectToAction("Index", "Home");
        }
        // GET: /Account/ExternalLoginFailure        
        /// <summary>
        /// Externals the login failure.
        /// </summary>
        /// <returns>
        /// Action Result
        /// </returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return this.View();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.applicationUserManager != null)
                {
                    this.applicationUserManager.Dispose();
                    this.applicationUserManager = null;
                }

                if (this.applicationSignInManager != null)
                {
                    this.applicationSignInManager.Dispose();
                    this.applicationSignInManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}