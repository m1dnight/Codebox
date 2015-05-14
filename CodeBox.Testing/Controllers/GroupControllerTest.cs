using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Models.Group;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeBox.Testing.Controllers
{
    [TestClass]
    public class GroupControllerTest
    {
        [TestMethod]
        public void CreateGroupTestPost()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            // Expect a redirect action
            var result = controller.Create(new Group());
            // If all went well we should get a redirect.
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));
        }

        [TestMethod]
        public void DeleteGroupTest()
        {
            //TODO
        }

        [TestMethod]
        public void ViewValidGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var result = controller.Details(1);
            // We should expect q view here.
            Assert.IsInstanceOfType(result, typeof (ViewResult));

            var response = ((ViewResult) result);
        }

        [TestMethod]
        public void ViewInValidGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var result = controller.Details(99999);
            // We should be redirected to the index.
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult) result).RouteValues["action"], "Index");
        }

        [TestMethod]
        public void RequestEditValidGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var result = controller.Edit(1);
            // We should expect q view here.
            Assert.IsInstanceOfType(result, typeof (ViewResult));

            var response = ((ViewResult) result);
        }

        [TestMethod]
        public void RequestEditInvalidGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var result = controller.Edit(99999);
            // We should be redirected to the index.
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult) result).RouteValues["action"], "Index");
        }

        [TestMethod]
        public void EditPostChanges()
        {
            // TODO
        }

        [TestMethod]
        public void TestAddUser()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("notadmin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("notadmin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;
            var result = controller.AddUserToGroup(new AddUserViewModel()
            {
                Mail = "testmail@mail.com", // Mail matches the one in sampledata!
                GroupId = 1,
            });
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "Index");
            var tempdatamessage = controller.TempData["message"];

            Assert.AreEqual(tempdatamessage, string.Format("{0} has been added to {1}!", "admin", "Test Group"));
        }

        [TestMethod]
        public void TestAddUserInvalidUser()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("notadmin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("notadmin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;
            var result = controller.AddUserToGroup(new AddUserViewModel()
            {
                Mail = "invalid@mail.com", // Mail does not match!
                GroupId = 1,
            });
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "Index");
            var tempdatamessage = controller.TempData["error"];

            Assert.AreEqual(tempdatamessage, string.Format("A user for {0} was not found!", "invalid@mail.com"));
        
        }

        [TestMethod]
        public void TestAddUserAlreadyInGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("notadmin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("notadmin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;
            var result = controller.AddUserToGroup(new AddUserViewModel()
            {
                Mail = "someemail@mail.com", // Mail does not match!
                GroupId = 1,
            });
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "Index");
            var tempdatamessage = controller.TempData["error"];

            Assert.AreEqual(tempdatamessage, "This user is already part of this group!");
        }

        [TestMethod]
        public void LeaveGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var result = controller.LeaveGroup(1);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "Index");
            var tempdatamessage = controller.TempData["message"];

            Assert.AreEqual(tempdatamessage, "You left Test Group!");
        }

        [TestMethod]
        public void LeaveGroupUnexistingGroup()
        {
            var controller = Helpers.CreateGroupController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var result = controller.LeaveGroup(456);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "Index");
            var tempdatamessage = controller.TempData["message"];

            Assert.AreEqual(tempdatamessage, "An error occured while leaving the group!");
        }

    }
}