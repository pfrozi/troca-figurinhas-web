using TrocaFigurinhas.Models.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using TrocaFigurinhas.Models.Persistence;
using System.Collections.Generic;

namespace TrocaFigurinhas.Tests
{
    
    
    /// <summary>
    ///This is a test class for OfertasBusinessTest and is intended
    ///to contain all OfertasBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OfertasBusinessTest
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
        ///A test for OfertasBusiness Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\PFROZI\\documents\\visual studio 2010\\Projects\\troca-figurinhas-web\\troca-figurinhas-web", "/")]
        [UrlToTest("http://localhost:50549/")]
        public void OfertasBusinessConstructorTest()
        {
            OfertasBusiness target = new OfertasBusiness();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for BuscarDesejadasPorNome
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\PFROZI\\documents\\visual studio 2010\\Projects\\troca-figurinhas-web\\troca-figurinhas-web", "/")]
        [UrlToTest("http://localhost:50549/")]
        public void BuscarDesejadasPorNomeTest()
        {
            OfertasBusiness target = new OfertasBusiness(); // TODO: Initialize to an appropriate value
            string user = string.Empty; // TODO: Initialize to an appropriate value
            string nomeFigurinha = string.Empty; // TODO: Initialize to an appropriate value
            List<Ofertas> expected = null; // TODO: Initialize to an appropriate value
            List<Ofertas> actual;
            actual = target.BuscarDesejadasPorNome(user, nomeFigurinha);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BuscarOfertadasPorNome
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\PFROZI\\documents\\visual studio 2010\\Projects\\troca-figurinhas-web\\troca-figurinhas-web", "/")]
        [UrlToTest("http://localhost:50549/")]
        public void BuscarOfertadasPorNomeTest()
        {
            OfertasBusiness target = new OfertasBusiness(); // TODO: Initialize to an appropriate value
            string user = string.Empty; // TODO: Initialize to an appropriate value
            string nomeFigurinha = string.Empty; // TODO: Initialize to an appropriate value
            List<Ofertas> expected = null; // TODO: Initialize to an appropriate value
            List<Ofertas> actual;
            actual = target.BuscarOfertadasPorNome(user, nomeFigurinha);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
