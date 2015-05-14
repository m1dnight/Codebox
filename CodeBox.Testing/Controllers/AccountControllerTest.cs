using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CodeBox.WebUI.Models.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        [TestMethod]
        public void EditProfileValidInputPost()
        {
            var controller = Helpers.CreateAccountController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            EditAccountDetailsViewModel model = new EditAccountDetailsViewModel
            {
                ImageData = null,
                ImageMimetype = "MimeType",
                Mail = "foo@bar.com",
                Name = "Test Name",
                OldPassword = "abc123",
                Password = "abc123",
                Surname = "De Troyer",
                UserId = 1
            };
            var result = controller.EditAccountDetails(model, null);
            // Assert the pop to be correct.
            var tempdatamessage = controller.TempData["message"];
            Assert.AreEqual("admin, your profile has been updated!", tempdatamessage);

        }

        [TestMethod]
        public void EditProfileInvalidInputPost()
        {
            var controller = Helpers.CreateAccountController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            EditAccountDetailsViewModel model = new EditAccountDetailsViewModel
            {
                ImageData = null,
                ImageMimetype = "MimeType",
                Mail = "foo@bar.com",
                Name = "Test Name",
                OldPassword = "wrongoldpassword",
                Password = "abc123",
                Surname = "De Troyer",
                UserId = 1
            };
            var result = controller.EditAccountDetails(model, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var response = ((ViewResult)result);
            // There should be an error in the viewdata
            var viewdata = response.ViewData;
            var errormsg = viewdata.ModelState[""].Errors.First().ErrorMessage;
            Assert.AreEqual("Password did not change! Is it strong (6 characters, 1 number and 1 special character) enough? ", errormsg);

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