using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frmworkPKC.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Builder;
using NSubstitute;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

namespace frmworkPKC.UnitTest
{
    [TestClass]
    public class StartUpTests
    {
        [TestMethod]
        public void StartUp_Tests()
        {
            IAppBuilder app = new AppBuilder();
            frmworkPKC.Web.Startup startUp = new frmworkPKC.Web.Startup();
            startUp.ConfigureAuth(app);
        }

        [TestMethod]
        public void ApplicationUser_Tests()
        {
            var interUser = Substitute.For<IUserStore<ApplicationUser>>();
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(interUser);
            ApplicationUser user = new ApplicationUser();
            user.GenerateUserIdentityAsync(manager);
        }
    }
}
