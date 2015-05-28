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
    public class SnippetList
    {

        [TestInitialize]
        public void LogIn()
        {
            Common.LogInRandomPerson007();
            Common.GoToUrl(Common.HOME_URL + "Snippet/List");
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        public static void MakeSnippetMenuVisible(IWebDriver driver, int snippetNr)
        {
            // http://stackoverflow.com/questions/17293914/how-to-perform-mouseover-function-in-selenium-webdriver-using-java
            driver.Navigate().GoToUrl(Common.HOME_URL + "Snippet/List");
            string hoverableElementXPath = "//*[@id=\"ContentMain\"]/div/div[" + snippetNr + "]/div[1]";
            IWebElement hoverableArea = driver.FindElement(By.XPath(hoverableElementXPath));
            Actions action = new Actions(driver);
            action.MoveToElement(hoverableArea).Build().Perform();
        }

        private void SnippetListMenuInvisible(IWebDriver driver)
        {
            IWebElement editIcon = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div[1]/div[2]/a[1]/img"));
            IWebElement deleteIcon = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div[1]/div[2]/a[2]/img"));
            int editHeight = editIcon.Size.Height;
            int editWidth = editIcon.Size.Width;
            int deleteHeight = editIcon.Size.Height;
            int deleteWidth = editIcon.Size.Width;
            Assert.AreEqual(0, editHeight);
            Assert.AreEqual(0, editWidth);
            Assert.AreEqual(0, deleteHeight);
            Assert.AreEqual(0, deleteWidth);

            MakeSnippetMenuVisible(driver, 1);

            // Refind this element to be sure we're using the correct one
            editIcon = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div[1]/div[2]/a[1]/img"));
            deleteIcon = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div[1]/div[2]/a[2]/img"));

            editHeight = editIcon.Size.Height;
            editWidth = editIcon.Size.Width;
            deleteHeight = editIcon.Size.Height;
            deleteWidth = editIcon.Size.Width;
            Assert.IsTrue(editHeight > 0);
            Assert.IsTrue(editWidth > 0);
            Assert.IsTrue(deleteHeight > 0);
            Assert.IsTrue(deleteWidth > 0);

            IWebElement editAnchor = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div[1]/div[2]/a[1]"));
            IWebElement deleteAnchor = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div[1]/div[2]/a[2]"));
            string editLink = editAnchor.GetAttribute("href");
            string deleteLink = deleteAnchor.GetAttribute("href");
            StringAssert.StartsWith(editLink, Common.HOME_URL + "Snippet/Edit?SnippetId=");
            StringAssert.StartsWith(deleteLink, Common.HOME_URL + "Snippet/DeleteSnippet?SnippetId=");
        }

        [TestMethod]
        public void TestSnippetListMenuVisible()
        {
            SnippetListMenuInvisible(Common.chromeDriver);
        }

    }
}
