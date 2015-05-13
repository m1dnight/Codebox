using System;
using System.Web.Mvc;
using CodeBox.WebUI.Models.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeBox.Testing.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void LoginTest()
        {
            var controller = Helpers.CreateAccountController();

            //////////////////////////
            // Test with valid user //
            //////////////////////////

            LogOnViewModel model = new LogOnViewModel();
            model.Password = "abc123";
            model.Username = "admin";
            model.ReturnUrl = null;

            var result = controller.LogIn(model);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var redirect = (RedirectToRouteResult)result;
            AssertRedirect(redirect, "List", "Snippet");

        }

        public void AssertRedirect(RedirectToRouteResult redirect, String action, String controller)
        {
            Assert.IsTrue(redirect.RouteValues.ContainsKey("action"));
            Assert.IsTrue(redirect.RouteValues.ContainsKey("controller"));
            Assert.AreEqual(action, redirect.RouteValues["action"].ToString());
            Assert.AreEqual(controller, redirect.RouteValues["controller"].ToString());

        }
    }
}
