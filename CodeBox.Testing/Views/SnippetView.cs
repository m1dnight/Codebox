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
            Common.chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Snippet/Create");
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        private void GoToSnippet(IWebDriver driver, int snippetNr)
        {
            string xPath = "//*[@id=\"ContentMain\"]/div/div[" + snippetNr + "]/div[1]/div[3]/a";
            IWebElement selectedSnippetLink = driver.FindElement(By.XPath(xPath));
            selectedSnippetLink.Click();
            
        }

        private void SnippetViewValidInsert1(IWebDriver driver)
        {
            SimpleSnippet snippet = new SimpleSnippet("a", "a", "a");
            SnippetNew.CreateSnippet(driver, snippet);
            GoToSnippet(driver, 1);

            IWebElement nameElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/h1"));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[2]"));
            IWebElement languageElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[12]"));
            IWebElement codeElement = driver.FindElement(By.XPath("//*[@id=\"codefield\"]/ol/li/span/span"));

            StringAssert.Contains("a", nameElement.Text);
            StringAssert.Contains("a", descriptionElement.Text);
            StringAssert.Contains("a", codeElement.Text);
            StringAssert.Contains("None", languageElement.Text);
            
        }

        private void SnippetViewValidInsert2(IWebDriver driver)
        {
            SimpleSnippet snippet = new SimpleSnippet("a longer name", "some java code", "description", "Java", true);
            SnippetNew.CreateSnippet(driver, snippet);
            GoToSnippet(driver, 1);

            IWebElement nameElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/h1"));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[2]"));
            IWebElement isPublicElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[8]/input"));
            IWebElement languageElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[12]"));
            IWebElement codeElement = driver.FindElement(By.XPath("//*[@id=\"codefield\"]/ol/li/span/span"));

            StringAssert.Contains("a longer name", nameElement.Text);
            StringAssert.Contains("some java code", codeElement.Text);
            StringAssert.Contains("description", descriptionElement.Text);
            StringAssert.Contains("Java", languageElement.Text);
            StringAssert.Contains("true", isPublicElement.GetAttribute("checked"));
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
