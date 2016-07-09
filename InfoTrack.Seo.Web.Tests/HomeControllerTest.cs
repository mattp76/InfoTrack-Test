using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoTrack.Seo.Web.Controllers;
using InfoTrack.Seo.Web.Models;
using InfoTrack.Seo.Web.Interfaces;
using Moq;

namespace InfoTrack.Seo.Web.Tests
{
    /// <summary>
    ///  Some basic testing for the home controller. I am using Moq to pass through a mocked version of the GoogleSearchHelper, along with a dummy response.
    /// </summary> 
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private SearchViewModel searchViewModel;
        private string url;
        private string keywords;
        private List<int> positions;

        [TestInitialize]
        public void Initialize() {

            positions = new List<int>{ 1, 2, 3 };
            url = "www.infotrack.com.au";
            keywords = "online term search";
    
            var mockGoogleSearchHelper = new Mock<IGoogleSearchPositionHelper>();
            mockGoogleSearchHelper.Setup<List<int>>(data => data.GetPosition(It.IsAny<string>(), It.IsAny<string>())).Returns(positions);
            this.controller = new HomeController(mockGoogleSearchHelper.Object);

            searchViewModel = new SearchViewModel{Keywords = keywords, Url = url};
        }

        [TestMethod]
        public void Test_Controller_Default_View()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_Controller_With_Model_Error()
        {
            controller.ModelState.AddModelError("fakeError", "fakeError");

            var result = controller.Index(searchViewModel) as ViewResult;

            Assert.IsTrue(!controller.ModelState.IsValid);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_Controller_With_Valid_Model_and_Result()
        {
            var result = controller.Index(searchViewModel) as ViewResult;

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.AreEqual(positions, searchViewModel.Positions);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
