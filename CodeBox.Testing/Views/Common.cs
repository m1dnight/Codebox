using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class Common
    {

        public const string HOME_URL = "http://localhost:64886/";
        public static ChromeDriver chromeDriver;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            chromeDriver = new ChromeDriver();
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            chromeDriver.Quit();
        }

        public static void CheckLink(IWebDriver driver, IWebElement link, string expectedUrl)
        {
            link.Click();
            StringAssert.Contains(expectedUrl, driver.Url);
            driver.Navigate().Back();
        }

        public static void LogInRandomPerson007()
        {
            chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Account/LogIn");
            string userName = "randomperson007";
            string password = "v3ry_str0ng_password!";
            PerformLogIn(chromeDriver, userName, password);
        }

        public static void LogOut()
        {
            chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Account/LogIn");
            IWebElement logOutButton = chromeDriver.FindElement(By.XPath("//*[@id=\"logout\"]/a"));
            logOutButton.Click();
        }

        public static void PerformLogIn(IWebDriver driver, string userName, string passWord)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement userNameBox = mainContent.FindElement(By.Id("Username"));
            IWebElement passwordBox = mainContent.FindElement(By.Id("Password"));
            IWebElement submitButton = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/p[1]/input[1]"));
            userNameBox.SendKeys(userName);
            passwordBox.SendKeys(passWord);
            submitButton.Click();
        }

        public static void SaveSnippet(IWebDriver driver, SimpleSnippet snippet)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            IWebElement codeBox = mainContent.FindElement(By.Id("Snippet_Code"));
            IWebElement publicCheckmark = mainContent.FindElement(By.Id("Snippet_Public"));
            IWebElement languageBox = mainContent.FindElement(By.Id("SelectedLanguageId"));
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));

            nameBox.Clear();
            nameBox.SendKeys(snippet._name);

            descriptionBox.Clear();
            descriptionBox.SendKeys(snippet._description);

            codeBox.Clear();
            codeBox.SendKeys(snippet._code);

            // Want to select checkmark but it's not selected yet -> click it
            // or the checkmark has been selected already but you want it deselected -> click it
            if ((publicCheckmark.Selected && (!snippet._isPublic)) ||
                ((!publicCheckmark.Selected) && snippet._isPublic))
            {
                publicCheckmark.Click();
            }
            SelectElement languagesSelect = new SelectElement(languageBox);
            foreach (IWebElement selectedElement in languagesSelect.AllSelectedOptions ) {
                languagesSelect.DeselectByText(selectedElement.Text);
            }
            languagesSelect.SelectByText(snippet._language);
            saveButton.Click();
        }

    }
}
