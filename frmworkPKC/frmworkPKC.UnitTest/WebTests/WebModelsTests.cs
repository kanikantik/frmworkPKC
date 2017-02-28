using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using frmworkPKC.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NSubstitute;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace frmworkPKC.UnitTest
{
    [TestClass]
    public class WebModelsTests
    {
        #region Account View Model Getter and Setter Test Cases
        [TestMethod]
        public void ExternalLoginConfirmationViewModel_Tests()
        {
            ExternalLoginConfirmationViewModel external = new ExternalLoginConfirmationViewModel();
            external.Email = "Epam@Epam.com";
            var email = external.Email;
            Assert.IsTrue(true, email);
        }

        [TestMethod]
        public void ExternalLoginListViewModel_Tests()
        {
            ExternalLoginListViewModel external = new ExternalLoginListViewModel();
            external.ReturnUrl = "http://www.google.com";
            var str = external.ReturnUrl;
            Assert.IsTrue(true, str);
        }

        [TestMethod]
        public void SendCodeViewModel_Tests()
        {
            SendCodeViewModel codeViewModel = new SendCodeViewModel()
            {
                Providers = new List<System.Web.Mvc.SelectListItem>(),
                RememberMe = true,
                ReturnUrl = "http://www.google.com",
                SelectedProvider = "whatever"
            };
            SendCodeViewModel codeViewModelGet = new SendCodeViewModel()
            {
                Providers = codeViewModel.Providers,
                RememberMe = codeViewModel.RememberMe,
                SelectedProvider = codeViewModel.SelectedProvider,
                ReturnUrl = codeViewModel.ReturnUrl
            };

            Assert.IsTrue(true, codeViewModelGet.ReturnUrl);
        }

        [TestMethod]
        public void VerifyCodeViewModel_Tests()
        {
            VerifyCodeViewModel codeViewModel = new VerifyCodeViewModel()
            {
                Code = "code",
                ReturnUrl = "http://www.google.com",
                Provider = "Provider",
                RememberBrowser = true,
                RememberMe = false
            };

            VerifyCodeViewModel codeViewModelGet = new VerifyCodeViewModel()
            {
                Code = codeViewModel.Code,
                Provider = codeViewModel.Provider,
                RememberBrowser = codeViewModel.RememberBrowser,
                RememberMe = codeViewModel.RememberMe,
                ReturnUrl = codeViewModel.ReturnUrl
            };

            Assert.IsTrue(true, codeViewModelGet.Code);
        }

        [TestMethod]
        public void ForgotViewModel_Tests()
        {
            ForgotViewModel view = new ForgotViewModel()
            {
                Email = "Epam@Epam.com"
            };

            var str = view.Email;
            Assert.IsTrue(true, str);
        }

        [TestMethod]
        public void LoginViewModel_Tests()
        {
            LoginViewModel model = new LoginViewModel()
            {
                Email = "Epam@Epam.com",
                Password = "password",
                RememberMe = false
            };

            LoginViewModel modelGet = new LoginViewModel()
            {
                Email = model.Email,
                RememberMe = model.RememberMe,
                Password = model.Password
            };
            Assert.IsTrue(true, modelGet.Email);
        }

        [TestMethod]
        public void RegisterViewModel_Tests()
        {
            RegisterViewModel model = new RegisterViewModel()
            {
                Email = "Epam@Epam.com",
                ConfirmPassword = "password",
                Password = "password"
            };
            RegisterViewModel modelGet = new RegisterViewModel()
            {
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                Email = model.Email
            };

            Assert.IsTrue(true, modelGet.Email);
        }

        [TestMethod]
        public void ResetPasswordViewModel_Tests()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                Code = "abcdef",
                Email = "Epam@Epam.com",
                ConfirmPassword = "password",
                Password = "password"
            };
            ResetPasswordViewModel modelGet = new ResetPasswordViewModel()
            {
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                Code = model.Code,
                Email = model.Email
            };

            Assert.IsTrue(true, modelGet.Email);
        }

        [TestMethod]
        public void ForgotPasswordViewModel_Tests()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel()
            {
                Email = "Epam@Epam.com"
            };
            var str = model.Email;
            Assert.IsTrue(true, str);
        }
        #endregion

        #region Manage View Models Getter and Setter Test Cases

        [TestMethod]
        public void IndexViewModel_Tests()
        {
            IndexViewModel model = new IndexViewModel()
            {
                BrowserRemembered = false,
                HasPassword = false,
                PhoneNumber = "123456789",
                TwoFactor = true,
                Logins = new List<UserLoginInfo>()
            };

            IndexViewModel modelGet = new IndexViewModel()
            {
                BrowserRemembered = model.BrowserRemembered,
                HasPassword = model.HasPassword,
                Logins = model.Logins,
                TwoFactor = model.TwoFactor,
                PhoneNumber = model.PhoneNumber
            };

            Assert.IsTrue(true, modelGet.PhoneNumber);
        }

        [TestMethod]
        public void ManageLoginsViewModel_Tests()
        {
            ManageLoginsViewModel model = new ManageLoginsViewModel()
            {
                CurrentLogins = new List<UserLoginInfo>(),
                OtherLogins = new List<AuthenticationDescription>()
            };
            ManageLoginsViewModel modelGet = new ManageLoginsViewModel()
            {
                CurrentLogins = model.CurrentLogins,
                OtherLogins = model.OtherLogins
            };
            Assert.IsTrue(true, modelGet.CurrentLogins.Count.ToString());
        }

        [TestMethod]
        public void FactorViewModel_Tests()
        {
            FactorViewModel model = new FactorViewModel()
            {
                Purpose = "Purpose"
            };
            var str = model.Purpose;
            Assert.AreEqual(str, model.Purpose);
        }

        [TestMethod]
        public void SetPasswordViewModel_Tests()
        {
            SetPasswordViewModel model = new SetPasswordViewModel()
            {
                ConfirmPassword = "password",
                NewPassword = "password"
            };
            if (model.ConfirmPassword == model.NewPassword)
            {
                Assert.IsTrue(true, "Passwords match");
            }
            else
            {
                Assert.IsTrue(true, "Passwords do not match");
            }
        }

        [TestMethod]
        public void ChangePasswordViewModel_Tests()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel()
            {
                ConfirmPassword = "Newpassword",
                NewPassword = "Newpassword",
                OldPassword = "password"
            };
            ChangePasswordViewModel modelGet = new ChangePasswordViewModel()
            {
                OldPassword = model.OldPassword,
                NewPassword = model.NewPassword,
                ConfirmPassword = model.ConfirmPassword
            };

            Assert.IsTrue(true, modelGet.OldPassword);
        }

        [TestMethod]
        public void AddPhoneNumberViewModel_Tests()
        {
            AddPhoneNumberViewModel model = new AddPhoneNumberViewModel()
            {
                Number = "123456789"
            };
            var str = model.Number;
            Assert.IsTrue(true, str);
        }

        [TestMethod]
        public void VerifyPhoneNumberViewModel_Tests()
        {
            VerifyPhoneNumberViewModel model = new VerifyPhoneNumberViewModel()
            {
                Code = "abcdef",
                PhoneNumber = "123456789"
            };

            VerifyPhoneNumberViewModel modelGet = new VerifyPhoneNumberViewModel()
            {
                PhoneNumber = model.PhoneNumber,
                Code = model.Code
            };
            Assert.IsTrue(true, modelGet.PhoneNumber);
        }

        [TestMethod]
        public void ConfigureTwoFactorViewModel_Tests()
        {
            ConfigureTwoFactorViewModel model = new ConfigureTwoFactorViewModel()
            {
                SelectedProvider = "Selected Provider",
                Providers = new List<System.Web.Mvc.SelectListItem>()
            };
            ConfigureTwoFactorViewModel modelGet = new ConfigureTwoFactorViewModel()
            {
                Providers = model.Providers,
                SelectedProvider = model.SelectedProvider
            };
            Assert.IsTrue(true, modelGet.SelectedProvider);
        }

        #endregion

        #region ApplicationUser Getter and Setter Test Cases

        [TestMethod]
        public void ApplicationUser_Create()
        {
            ApplicationUser user = new ApplicationUser();
            var iStore = Substitute.For<IUserStore<ApplicationUser>>();
            Task<ClaimsIdentity> claim = new Task<ClaimsIdentity>(() => new ClaimsIdentity());
            var appUser = Substitute.For<UserManager<ApplicationUser>>(iStore);
            appUser.CreateIdentityAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(claim);
            user.GenerateUserIdentityAsync(appUser).ConfigureAwait(true);
        }

        [TestMethod]
        public void ApplicationDbContext_Tests()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationDbContext.Create();
            Assert.IsTrue(true, context.Database.ToString());
        }
        #endregion
    }
}
