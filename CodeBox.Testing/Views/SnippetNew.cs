using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class SnippetNew
    {

        [ClassInitialize]
        public static void LogIn(TestContext context)
        {
            Common.LogInRandomPerson007();
        }

        [ClassCleanup]
        public static void LogOut()
        {
            Common.LogOut();
        }

        [TestInitialize]
        public void NavigateToCreatePage()
        {
            Common.chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "/Snippet/Create");
        }

        private void CheckSnippet(IWebDriver driver, string code, string description, string name, string language)
        {
            string capitalizedName = name.First().ToString().ToUpper() + String.Join("", name.Skip(1));
            IWebElement descriptionText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[4]/p"));
            IWebElement languageText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[2]"));
            IWebElement nameText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[3]/a"));
            IWebElement insertionSucceededMessage = driver.FindElement(By.Id("message"));
            StringAssert.Contains(description, descriptionText.Text);
            StringAssert.Contains(language, languageText.Text);
            StringAssert.Contains(capitalizedName, nameText.Text);
            StringAssert.Contains(name + " has been saved!", insertionSucceededMessage.Text);
        }

        private void SnippetNewContent(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            StringAssert.Contains("New Snippet", mainContent.FindElement(By.XPath("//h1[1]")).Text);

            StringAssert.Contains("Name", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[1]/label")).Text);
            StringAssert.Contains("Description", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[3]/label")).Text);
            StringAssert.Contains("Code", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[5]/label")).Text);
            StringAssert.Contains("Public", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[1]")).Text);
            StringAssert.Contains("Syntax:", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[3]")).Text);
            StringAssert.Contains("Share with group (optional):", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[5]")).Text);
            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            IWebElement codeBox = mainContent.FindElement(By.Id("Snippet_Code"));
            IWebElement publicCheckmark = mainContent.FindElement(By.Id("Snippet_Public"));
            IWebElement languageBox = mainContent.FindElement(By.Id("SelectedLanguageId"));
            IWebElement groupBox = mainContent.FindElement(By.Id("SelectedGroupId"));
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));
            IWebElement listLink = mainContent.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/a"));
            Common.CheckLink(driver, listLink, Common.HOME_URL + "Snippet/List");
        }

        private void SnippetNewEmptyCodeInsert(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            nameBox.SendKeys("a");
            descriptionBox.SendKeys("a");
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));
            saveButton.Click();

            IWebElement codeError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[6]/span"));
            StringAssert.Contains("No empty snippets allowed!", codeError.Text);
        }

        private void SnippetNewEmptyNameInsert(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            descriptionBox.SendKeys("a");
            saveButton.Click();

            IWebElement nameError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[2]/span/span"));
            StringAssert.Contains("A Name is required!", nameError.Text);
        }

        private void SnippetNewValidInsert(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));

            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            IWebElement codeBox = mainContent.FindElement(By.Id("Snippet_Code"));
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));

            string code = "a";
            string description = "a";
            string name = "a";

            nameBox.SendKeys(name);
            descriptionBox.SendKeys(description);
            codeBox.SendKeys(code);
            saveButton.Click();

            StringAssert.Contains(Common.HOME_URL + "Snippet/List", driver.Url);
            CheckSnippet(driver, code, description, name, "None");
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
