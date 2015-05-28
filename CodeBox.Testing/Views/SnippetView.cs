using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class SnippetView
    {

        [TestInitialize]
        public void LogIn()
        {
            Common.LogInRandomPerson007();
            Common.GoToUrl(Common.HOME_URL + "Snippet/Create");
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        public static void GoToSnippetView(IWebDriver driver, int snippetNr)
        {
            // Make sure you're in the Snippet.List view
            driver.Navigate().GoToUrl(Common.HOME_URL + "Snippet/List");

            string xPath = "//*[@id=\"ContentMain\"]/div/div[" + snippetNr + "]/div[1]/div[3]/a";
            IWebElement selectedSnippetLink = driver.FindElement(By.XPath(xPath));
            selectedSnippetLink.Click();
        }

        public static void CheckSnippet(IWebDriver driver, SimpleSnippet snippet)
        {
            IWebElement nameElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/h1"));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[2]"));
            IWebElement isPublicElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[8]/input"));
            IWebElement languageElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[12]"));
            IWebElement codeElement = driver.FindElement(By.XPath("//*[@id=\"codefield\"]/ol/li/span/span"));

            StringAssert.Contains(snippet.Name(), nameElement.Text);
            StringAssert.Contains(snippet.Code(), codeElement.Text);
            StringAssert.Contains(snippet.Description(), descriptionElement.Text);
            StringAssert.Contains(snippet.Language(), languageElement.Text);
            Assert.AreEqual(snippet.IsPublic(), isPublicElement.Selected);
        }

        private void SnippetViewValidInsert1(IWebDriver driver)
        {
            SimpleSnippet snippet = new SimpleSnippet("a", "a", "a");
            SnippetNew.CreateSnippet(driver, snippet);
            GoToSnippetView(driver, 1);
            CheckSnippet(driver, snippet);
            
        }

        private void SnippetViewValidInsert2(IWebDriver driver)
        {
            SimpleSnippet snippet = new SimpleSnippet("a longer name", "some java code", "description", "Java", true);
            SnippetNew.CreateSnippet(driver, snippet);
            GoToSnippetView(driver, 1);
            CheckSnippet(driver, snippet);
        }

        [TestMethod]
        public void TestSnippetViewValidInsert1()
        {
            SnippetViewValidInsert1(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetViewValidInsert2()
        {
            SnippetViewValidInsert2(Common.chromeDriver);
        }

    }
}
