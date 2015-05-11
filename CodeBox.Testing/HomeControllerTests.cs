using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;
using CodeBox.WebUI.Controllers;
using CodeBox.WebUI.Models.Home;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeBox.Testing
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void SummaryTest()
        {
            // Create a fake repository.
            var moqSnippets = new Mock<ISnippetRepository>();
            moqSnippets.Setup(m => m.Snippets).Returns(SampleData.SnippetList.AsQueryable());

            var moqUsers = new Mock<IUserRepository>();
            moqUsers.Setup(m => m.Users).Returns(SampleData.userdata.AsQueryable());

            // Create a controller to test the summary data.
            HomeController hc = new HomeController(moqSnippets.Object, moqUsers.Object);

            IndexViewModel x = (IndexViewModel) hc.Summary().Model;

            Assert.AreEqual(x.PublicSnippets.Count, 2);
            Assert.AreEqual(x.SnippetCount, 2);
            Assert.AreEqual(x.Usercount, 1);
            Assert.AreEqual(x.UsersOnline, 0);
        }
    }
}
