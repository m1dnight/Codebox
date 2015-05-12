using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;
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
            var identity = new GenericIdentity("admin");
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
    }
}
