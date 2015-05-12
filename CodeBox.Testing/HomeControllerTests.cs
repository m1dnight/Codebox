using System;
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
            var hc = Helpers.CreateHomeController();
            // Call the summary method.
            IndexViewModel x = (IndexViewModel)hc.Summary().Model;
            // Assertions of the summary based on the sample data.
            Assert.AreEqual(3, x.PublicSnippets.Count);
            Assert.AreEqual(6, x.SnippetCount);
            Assert.AreEqual(1, x.Usercount);
            Assert.AreEqual(0, x.UsersOnline);
        }



        /// <summary>
        /// Tests a few snippets to make sure that pubilc snippets can be viewed without logging in.
        /// </summary>
        [TestMethod]
        public void PublicSnippetTest()
        {
            var hc = Helpers.CreateHomeController();
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
    }
}
