using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Models.Snippet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeBox.Testing.Controllers
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
            int total = ((IEnumerable<Snippet>)model).Count();
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
            SnippetCRUDViewModel model = (SnippetCRUDViewModel)((ViewResult)result).Model;

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

        [TestMethod]
        public void EditPostTest()
        {
            ////////////////////////////////////////
            // Test posting an empty snippet name.//
            ////////////////////////////////////////
            var controller = Helpers.CreateSnippetController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;


            SnippetCRUDViewModel model = new SnippetCRUDViewModel();
            model.Snippet = new Snippet() { Name = "" };
            var result = controller.Edit(model);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var response = ((ViewResult)result);
            // There should be an error in the viewdata
            var viewdata = response.ViewData;
            var errormsg = viewdata.ModelState["Snippet.Name"].Errors.First().ErrorMessage;
            Assert.AreEqual("Name is required!", errormsg);

            /////////////////////////////////////
            // Test posting empty code snippet //
            /////////////////////////////////////
            // The error message from above will still be in the ModelState!
            // -> Create new controller from scratch.
            controller = Helpers.CreateSnippetController();
            controllerContext = new Mock<ControllerContext>();
            principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            model = new SnippetCRUDViewModel();
            model.Snippet = new Snippet() { Name = "Unimportant name", Code = "" };
            result = controller.Edit(model);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            response = ((ViewResult)result);
            // There should be an error in the viewdata
            viewdata = response.ViewData;
            errormsg = viewdata.ModelState["Snippet.Code"].Errors.First().ErrorMessage;
            Assert.AreEqual("No empty snippets allowed!", errormsg);

            //////////////////////////////
            // Test modifying a snippet //
            //////////////////////////////
            // The error message from above will still be in the ModelState!
            // -> Create new controller from scratch.
            controller = Helpers.CreateSnippetController();
            controllerContext = new Mock<ControllerContext>();
            principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            model = new SnippetCRUDViewModel();
            model.Snippet = SampleData.SnippetList.First(); // We own the first snippet in that list (user admin).
            result = controller.Edit(model);
            // If all went well we should get a redirect.
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void CreateSnippetGetTest()
        {
            var controller = Helpers.CreateSnippetController();
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            principal.Setup(p => p.IsInRole("admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("admin");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            ViewResult result = controller.Create();
            var model = result.Model;
            Assert.AreEqual("Edit", result.ViewName);
        }

    }
}