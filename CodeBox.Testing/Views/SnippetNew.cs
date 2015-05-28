using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class SnippetNew
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

        public static void CreateSnippet(IWebDriver driver, SimpleSnippet snippet)
        {
            Common.GoToUrl(Common.HOME_URL + "Snippet/Create");
            Common.SaveSnippet(driver, snippet);
        }

        private void CheckSnippet(IWebDriver driver, SimpleSnippet snippet)
        {
            string capitalizedName = snippet._name.First().ToString().ToUpper() + String.Join("", snippet._name.Skip(1));
            IWebElement descriptionText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[4]/p"));
            IWebElement languageText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[2]"));
            IWebElement nameText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[3]/a"));
            IWebElement insertionSucceededMessage = driver.FindElement(By.Id("message"));
            StringAssert.Contains(snippet._description, descriptionText.Text);
            StringAssert.Contains(snippet._language, languageText.Text);
            StringAssert.Contains(capitalizedName + " (0)", nameText.Text);
            StringAssert.Contains(snippet._name + " has been saved!", insertionSucceededMessage.Text);
        }

        private void SnippetNewContent(IWebDriver driver)
        {
            StringAssert.Contains("New Snippet", driver.FindElement(By.XPath("//h1[1]")).Text);
            StringAssert.Contains("Name", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[1]/label")).Text);
            StringAssert.Contains("Description", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[3]/label")).Text);
            StringAssert.Contains("Code", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[5]/label")).Text);
            StringAssert.Contains("Public", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[1]")).Text);
            StringAssert.Contains("Syntax:", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[3]")).Text);
            StringAssert.Contains("Share with group (optional):", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[5]")).Text);
            IWebElement nameBox = driver.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = driver.FindElement(By.Id("Snippet_Description"));
            IWebElement codeBox = driver.FindElement(By.Id("Snippet_Code"));
            IWebElement publicCheckmark = driver.FindElement(By.Id("Snippet_Public"));
            IWebElement languageBox = driver.FindElement(By.Id("SelectedLanguageId"));
            IWebElement groupBox = driver.FindElement(By.Id("SelectedGroupId"));
            IWebElement saveButton = driver.FindElement(By.Id("SnippetSubmit"));
            IWebElement listLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/a"));
            Common.CheckLink(driver, listLink, Common.HOME_URL + "Snippet/List");
        }

        private void SnippetNewEmptyCodeInsert(IWebDriver driver)
        {
            IWebElement nameBox = driver.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = driver.FindElement(By.Id("Snippet_Description"));
            nameBox.SendKeys("a");
            descriptionBox.SendKeys("a");
            IWebElement saveButton = driver.FindElement(By.Id("SnippetSubmit"));
            saveButton.Click();

            IWebElement codeError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[6]/span"));
            StringAssert.Contains("No empty snippets allowed!", codeError.Text);
        }

        private void SnippetNewEmptyNameInsert(IWebDriver driver)
        {
            SimpleSnippet snippet = new SimpleSnippet("", "", "a");
            CreateSnippet(driver, snippet);

            IWebElement nameError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[2]/span/span"));
            StringAssert.Contains("A Name is required!", nameError.Text);
        }

        private void SnippetNewValidInsert(IWebDriver driver)
        {
            string code = "a";
            string description = "a";
            string name = "a";
            SimpleSnippet snippet = new SimpleSnippet(name, code, description);
            CreateSnippet(driver, snippet);

            StringAssert.Contains(Common.HOME_URL + "Snippet/List", driver.Url);
            CheckSnippet(driver, snippet);
            SnippetDelete.DeleteSnippet(driver, 1);
        }

        [TestMethod]
        public void TestSnippetNewContent()
        {
            SnippetNewContent(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetNewEmptyCodeInsert()
        {
            SnippetNewEmptyCodeInsert(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetNewEmptyNameInsert()
        {
            SnippetNewEmptyNameInsert(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetNewValidInsert()
        {
            SnippetNewValidInsert(Common.chromeDriver);
        }
    }
}
