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
    [TestClass]
    public class HomeControllerTest
    {

        private HomeController controller;
        private string url;
        private string keywords;
        private List<int> positions;

        [TestInitialize]
        public void Initialize() {

            positions = new List<int>{ 1, 2, 3 };
            url = "www.infotrack.com.au";
            keywords = "online term search";
    
            var mockGoogleSearchHelper = new Mock<IGoogleSearchPositionHelper>();
            mockGoogleSearchHelper.Setup<List<int>>(data => data.GetPosition(url, keywords)).Returns(positions);

            this.controller = new HomeController(mockGoogleSearchHelper.Object);
        }

        [TestMethod]
        public void test_controller_view_result()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void test_controller_with_model_error()
        {
            SearchViewModel searchViewModel = new SearchViewModel
            {
                Keywords = null,
                Url = null
            };

            controller.ModelState.AddModelError("fakeError", "fakeError");

            var result = controller.Index(searchViewModel) as ViewResult;

            Assert.IsTrue(!controller.ModelState.IsValid);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void test_controller_with_valid_model_and_result()
        {
            SearchViewModel searchViewModel = new SearchViewModel
            {
                Keywords = keywords,
                Url = url
            };

            var result = controller.Index(searchViewModel) as ViewResult;

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.AreEqual(positions, result.ViewData["Positions"]);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
