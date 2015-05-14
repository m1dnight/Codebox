using System;
using System.Linq;
using System.Web.Mvc;
using CodeBox.WebUI.Models.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeBox.Testing.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void LoginValidUser()
        {
            var controller = Helpers.CreateAccountController();

            LogOnViewModel model = new LogOnViewModel();
            model.Password = "abc123";
            model.Username = "admin";
            model.ReturnUrl = null;

            var result = controller.LogIn(model);
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));
            var redirect = (RedirectToRouteResult) result;
            AssertRedirect(redirect, "List", "Snippet");
        }

        [TestMethod]
        public void LoginInvalidPassword()
        {
            var controller = Helpers.CreateAccountController();
            var model = new LogOnViewModel
            {
                Password = "WROOONG!",
                Username = "admin",
                ReturnUrl = null
            };

            var result = controller.LogIn(model);
            var response = ((ViewResult) result);
            // There should be an error in the viewdata
            var viewdata = response.ViewData;
            Assert.IsNotNull(viewdata.ModelState["Password"]);
            var errormsg = viewdata.ModelState["Password"].Errors.First().ErrorMessage;
            Assert.AreEqual("The password is not correct!", errormsg);
        }

        [TestMethod]
        public void LoginInvalidUsername()
        {
            ////////////////////////////////
            // Test with invalid username //
            ////////////////////////////////
            var controller = Helpers.CreateAccountController();
            var model = new LogOnViewModel
            {
                Password = "abc123",
                Username = "nonexistinguser",
                ReturnUrl = null
            };

            var result = controller.LogIn(model);
            var response = ((ViewResult) result);
            // There should be an error in the viewdata
            var viewdata = response.ViewData;
            Assert.IsNotNull(viewdata.ModelState["Username"]);
            var errormsg = viewdata.ModelState["Username"].Errors.First().ErrorMessage;
            Assert.AreEqual("User does not exist!", errormsg);
        }

        [TestMethod]
        public void RegisterValidInput()
        {
            var controller = Helpers.CreateAccountController();
            var model = new RegisterViewModel
            {
                Username = "validusername",
                Mail = "foo@bar.com",
                Password = "validpassword1%"
            };
            var result = controller.Register(model);
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));
            var redirect = (RedirectToRouteResult) result;
            AssertRedirect(redirect, "LogIn", "Account");
        }


        [TestMethod]
        public void RegisterInvalidInput()
        {
            // Testing invalid input does not make sense as all the logic is contained within the CustomMembership provider.
            // Unit tests for that class will satisfy.e
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