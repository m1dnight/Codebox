using CodeBox.Domain.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Testing
{
    
    
    /// <summary>
    ///This is a test class for EFSnippetRepositoryTest and is intended
    ///to contain all EFSnippetRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EFSnippetRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddUserToSnippet
        ///</summary>
        [TestMethod()]
        public void AddUserToSnippetTest()
        {
            EFSnippetRepository target = new EFSnippetRepository(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            Snippet snippet = null; // TODO: Initialize to an appropriate value
            target.AddUserToSnippet(username, snippet);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
