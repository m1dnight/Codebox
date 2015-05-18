using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class SnippetDelete
    {

        [TestInitialize]
        public void LogIn()
        {
            Common.LogInRandomPerson007();
            Common.chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "/Snippet/List");
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        public static void DeleteSnippet(IWebDriver driver, int snippetNr)
        {
            SnippetList.MakeSnippetMenuVisible(driver, snippetNr);
            string deleteLinkXPath = "//*[@id=\"ContentMain\"]/div/div[" + snippetNr + "]/div[2]/a[2]/img";
            IWebElement selectedSnippetDeleteLink = driver.FindElement(By.XPath(deleteLinkXPath));
            selectedSnippetDeleteLink.Click();
        }

        private void SnippetDeleteValid(IWebDriver driver)
        {
            string toBeDeleted = "to be deleted";
            SimpleSnippet snippet = new SimpleSnippet(toBeDeleted, toBeDeleted, toBeDeleted);
            SnippetNew.CreateSnippet(driver, snippet);
            DeleteSnippet(driver, 1);

            IWebElement deletedMessage = driver.FindElement(By.XPath("//*[@id=\"message\"]"));
            StringAssert.Contains(toBeDeleted + " has been deleted!", deletedMessage.Text);
        }

        [TestMethod]
        public void TestSnippetDeleteValid()
        {
            SnippetDeleteValid(Common.chromeDriver);
        }

    }
}
