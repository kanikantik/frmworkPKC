using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frmworkPKC.Web;
using frmworkPKC.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NSubstitute;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace frmworkPKC.UnitTest
{
    [TestClass]
    public class ApplicationSignInManagerTests
    {
        ApplicationUserManager applicationUserManager;
        IAuthenticationManager authenticationManager;
        ApplicationSignInManager applicationSignInManager;
        IUserStore<ApplicationUser> store;
        ApplicationUser applicationUser;

        public ApplicationSignInManagerTests()
        {
            store = Substitute.For<IUserStore<ApplicationUser>>();
            applicationUserManager = new ApplicationUserManager(store);
            authenticationManager = Substitute.For<IAuthenticationManager>();
            applicationSignInManager = new ApplicationSignInManager(applicationUserManager, authenticationManager);
            applicationUser = new ApplicationUser();
        }

        [TestMethod]
        public void CreateUserIdentityAsync_Tests()
        {
            applicationSignInManager.CreateUserIdentityAsync(applicationUser);
        }
    }
}
