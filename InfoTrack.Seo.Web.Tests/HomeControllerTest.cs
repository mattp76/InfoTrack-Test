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
        private SearchModel searchModel;

        [TestInitialize]
        public void Initialize() {

            List<int> positions = new List<int>(new int[] { 1, 2, 3 });
            string url = "https://www.infotrack.com.au";
            string keywords = "online term search";
    
            var mockGoogleSearchHelper = new Mock<IGoogleSearchPositionHelper>();
            var mockSearchModel = new Mock<ISearchModel>();
    
            mockGoogleSearchHelper.Setup(data => data.GetPosition(url, keywords)).Returns(positions);
            mockSearchModel.Setup(data => data.Keywords).Returns(keywords);
            mockSearchModel.Setup(data => data.Url).Returns(url);
            mockSearchModel.Setup(data => data.Positions).Returns(positions);

            //this.searchModel = mockSearchModel.Object;
            this.controller = new HomeController(mockGoogleSearchHelper.Object);
            this.searchModel = new SearchModel();
        }

        [TestMethod]
        public void TestIndexView()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }


        [TestMethod]
        public void TestIndexPostView()
        {
            var result = controller.Index(searchModel) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
