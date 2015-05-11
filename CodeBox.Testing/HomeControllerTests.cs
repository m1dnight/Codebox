﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
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
        /// <summary>
        /// Tests the home controller Summary method. 
        /// By using the sample data in SampleData.cs the method tests if the proper
        /// values are returned.
        /// </summary>
        [TestMethod]
        public void SummaryTest()
        {
            var hc = CreateHomeController();
            // Call the summary method.
            IndexViewModel x = (IndexViewModel)hc.Summary().Model;
            // Assertions of the summary based on the sample data.
            Assert.AreEqual(x.PublicSnippets.Count, 2);
            Assert.AreEqual(x.SnippetCount, 3);
            Assert.AreEqual(x.Usercount, 1);
            Assert.AreEqual(x.UsersOnline, 0);
        }



        /// <summary>
        /// Tests a few snippets to make sure that pubilc snippets can be viewed without logging in.
        /// </summary>
        [TestMethod]
        public void PublicSnippetTest()
        {
            var hc = CreateHomeController();
            ////////////////////////////
            // Test a public snippet. //
            ////////////////////////////
            var snipp = hc.PublicSnippet(1);

            // If we fetched a public snipet we have a viewresult
            Assert.IsInstanceOfType(snipp, typeof(ViewResult));

            Snippet model = (Snippet)((ViewResult)snipp).Model;
            Assert.AreEqual(model.SnippetId, 1);
            ////////////////////////////
            // Test a private snippet //
            ////////////////////////////
            snipp = hc.PublicSnippet(3);
            Assert.IsInstanceOfType(snipp, typeof(RedirectToRouteResult));
        }

        ///////////////
        /// HELPERS ///
        ///////////////
        private static HomeController CreateHomeController()
        {
            // Create a fake repository.
            var moqSnippets = new Mock<ISnippetRepository>();
            moqSnippets.Setup(m => m.Snippets).Returns(SampleData.SnippetList.AsQueryable());
            var moqUsers = new Mock<IUserRepository>();
            moqUsers.Setup(m => m.Users).Returns(SampleData.userdata.AsQueryable());

            // Create the home controller.
            HomeController hc = new HomeController(moqSnippets.Object, moqUsers.Object);
            return hc;
        }
    }
}
