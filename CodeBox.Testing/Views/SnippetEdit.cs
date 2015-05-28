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
    public class SnippetEdit
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

        private void CreateEditAndCheckSnippet(IWebDriver driver, SimpleSnippet newSnippet, SimpleSnippet editedSnippet)
        {
            SnippetNew.CreateSnippet(driver, newSnippet);
            GoToSnippetEditPage(driver, 1);
            Common.SaveSnippet(driver, editedSnippet);
            SnippetView.GoToSnippetView(driver, 1);
            SnippetView.CheckSnippet(driver, editedSnippet);
        }

        private void GoToSnippetEditPage(IWebDriver driver, int snippetNr)
        {
            // Make sure you're in the Snippet.List view
            driver.Navigate().GoToUrl(Common.HOME_URL + "Snippet/List");

            // Scrape the url ot the edit view of the given snippet
            string xPath = "//*[@id=\"ContentMain\"]/div/div[" + snippetNr + "]/div[2]/a[1]";
            IWebElement anchorToPage = driver.FindElement(By.XPath(xPath));
            string linkToPage = anchorToPage.GetAttribute("href");
            driver.Navigate().GoToUrl(linkToPage);
        }

        private void SnippetEditValid1(IWebDriver driver)
        {
            SimpleSnippet newSnippet = new SimpleSnippet("edit", "edit", "edit", "C", false);
            SimpleSnippet editedSnippet = new SimpleSnippet("edited name", "edited code", "edited description", "Java", true);
            CreateEditAndCheckSnippet(driver, newSnippet, editedSnippet);
        }

        private void SnippetEditValid2(IWebDriver driver)
        {
            SimpleSnippet newSnippet = new SimpleSnippet("some other name", "<html>important html code </html>", "a very important website", "HTML", false);
            SimpleSnippet editedSnippet = new SimpleSnippet("some other name", "I have no idea what CSS code might look like", "this is still a very important website", "CSS", false);
            CreateEditAndCheckSnippet(driver, newSnippet, editedSnippet);
        }

        private void SnippetEditValid3(IWebDriver driver)
        {
            SimpleSnippet newSnippet = new SimpleSnippet("hello world", "print \"hello world\"", "hello world in Python", "HTML", true);
            SimpleSnippet editedSnippet = new SimpleSnippet("hello world", "Console.WriteLine(\"hello world\");", "hello world in C#", "C#", false);
            CreateEditAndCheckSnippet(driver, newSnippet, editedSnippet);
        }

        [TestMethod]
        public void TestSnippetEditValid1()
        {
            SnippetEditValid1(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetEditValid2()
        {
            SnippetEditValid2(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetEditValid3()
        {
            SnippetEditValid3(Common.chromeDriver);
        }

    }
}
