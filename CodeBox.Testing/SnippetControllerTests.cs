using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Models.Snippet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeBox.Testing
{
    [TestClass]
    public class SnippetControllerTests
    {
        [TestMethod]
        public void ListTest()
        {
            //var identity = new GenericIdentity("admin");
            var controller = Helpers.CreateSnippetController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            var model = controller.List().Model;
            int total = ((IEnumerable<Snippet>) model).Count();
            Assert.AreEqual(total, 3);
        }

        [TestMethod]
        public void EditTest()
        {
            //var identity = new GenericIdentity("admin");
            var controller = Helpers.CreateSnippetController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;
            ////////////////////////////
            // Call a snippet we own. //
            ////////////////////////////
            Object result = controller.Edit(1);
            // Ensure we can edit it (the type is a view)
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            SnippetCRUDViewModel model = (SnippetCRUDViewModel) ((ViewResult) result).Model;

            Assert.AreEqual(1, model.Snippet.SnippetId);
            Assert.AreEqual(1, model.Groups.Count);
            ///////////////////////////////////
            // Call a snippet we do not own. //
            ///////////////////////////////////
            result = controller.Edit(10);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            /////////////////////////////////
            // Call an unexisting snippet. //
            /////////////////////////////////
            result = controller.Edit(10000);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}