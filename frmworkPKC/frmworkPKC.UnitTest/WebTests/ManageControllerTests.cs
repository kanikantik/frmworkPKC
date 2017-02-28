using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using frmworkPKC.Web;
using frmworkPKC.Web.Controllers;
using frmworkPKC.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NSubstitute;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace frmworkPKC.UnitTest
{
    [TestClass]
    public class ManageControllerTests
    {
        private ApplicationUserManager userManagerMock;
        private ApplicationSignInManager signInManagerMock;
        IAuthenticationManager authenticateManager;
        private ManageController manageController;
        IUserStore<ApplicationUser> applicationUser;
        IPrincipal identityMock;

        public ManageControllerTests()
        {
            identityMock = Substitute.For<IPrincipal>();
            authenticateManager = Substitute.For<IAuthenticationManager>();
            applicationUser = Substitute.For<IUserStore<ApplicationUser>>();
            userManagerMock = Substitute.For<ApplicationUserManager>(applicationUser);
            signInManagerMock = Substitute.For<ApplicationSignInManager>(userManagerMock, authenticateManager);
            manageController = new ManageController();
        }

        [TestMethod]
        public void EnableTwoFactorAuthentication_Tests()
        {
            var result = manageController.Index(ManageController.ManageMessageId.AddPhoneSuccess);
            //identityMock.Setup(x => x.Get)
        }

        [TestMethod]
        public void RemoveLogin_Tests()
        {
            var strArray = new string[] { };
            Task<IdentityResult> mockTask = new Task<IdentityResult>(() => new IdentityResult(strArray));
            Task<ApplicationUser> mockUser = new Task<ApplicationUser>(() => new ApplicationUser());
            var task = new Task(() => { });
            userManagerMock.RemoveLoginAsync(Arg.Any<string>(), Arg.Any<UserLoginInfo>()).Returns(mockTask);
            userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(mockUser);
            signInManagerMock.SignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<bool>(), Arg.Any<bool>()).Returns(task);
            var result = manageController.RemoveLogin("", "");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddPhoneNumber_Tests()
        {
            manageController.AddPhoneNumber();
            Assert.IsTrue(true, "true");
        }

        [TestMethod]
        public void ChangePassword_Tests()
        {
            manageController.ChangePassword();
        }

        [TestMethod]
        public void SetPassword_Tests()
        {
            manageController.SetPassword();
        }
    }
}
