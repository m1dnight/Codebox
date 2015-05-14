using System;
using System.Web.Security;
using CodeBox.WebUI.Infrastructure.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeBox.Testing.Controllers
{
    [TestClass]
    public class VariousTests
    {
        [TestMethod]
        public void TestEmailValidation()
        {
        }
        [TestMethod]
        public void TestPasswordValidation()
        {
            DerivedMembershipController mp = new DerivedMembershipController();
            String[] valid = new[] {"abc123%"};
            String[] invalid = new[] {"abc123", "short", "nospecialchars", "nonumbers"};
            foreach (String s in valid)
            {
                ValidatePasswordEventArgs args = new ValidatePasswordEventArgs("admin", s, true);
                mp.ValidatePassword(args);
                Assert.AreEqual(false, args.Cancel);
            }
            foreach (String s in invalid)
            {
                ValidatePasswordEventArgs args = new ValidatePasswordEventArgs("admin", s, true);
                mp.ValidatePassword(args);
                Assert.AreEqual(true, args.Cancel);
            }


        }
    }
}
