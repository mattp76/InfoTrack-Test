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
using log4net;
using InfoTrack.Seo.Web.Helpers;

namespace InfoTrack.Seo.Web.Tests
{
    /// <summary>
    ///  I felt it would be cool to try and test around the GoogleSearchHelper. Moq is used to mock out the GoogleClient and test around a dummy google response.
    /// </summary> 
    [TestClass]
    public class GoogleSearchPositionHelperTest
    {
        private string html;
        private string url;
        private string keywords;
        private ILog logger;

        [TestInitialize]
        public void Initialize()
        {
            url = "www.infotrack.com.au";
            keywords = "online term search";
            logger = null;
        }

        [TestMethod]
        public void Test_Google_Search_Helper_Result()
        {
            html = "<!doctype html><html><body><h3><a href='http://www.google.com.au'>http://www.google.com.au</a></h3><h3><a href='http://www.infotrack.com.au'>http://www.infotrack.com.au</a></h3><h3><a href='http://www.yahoo.com.au'>http://www.yahoo.com.au</a></h3></body></html>";

            var mockGoogleClient = new Mock<IGoogleClient>();
            mockGoogleClient.Setup<string>(data => data.getGoogleResponse(It.IsAny<string>())).Returns(html);

            GoogleSearchPositionHelper googleHelper = new GoogleSearchPositionHelper(logger, mockGoogleClient.Object);

            var result = googleHelper.GetPosition(keywords, url);
  
            Assert.AreEqual(1, result[0]);
        }

        [TestMethod]
        public void Test_Google_Search_Helper_No_Result()
        {

            html = "<!doctype html><html><body><h3><a href='http://www.google.com.au'>http://www.google.com.au</a></h3></body></html>";
   
            var mockGoogleClient = new Mock<IGoogleClient>();
            mockGoogleClient.Setup<string>(data => data.getGoogleResponse(It.IsAny<string>())).Returns(html);

            GoogleSearchPositionHelper googleHelper = new GoogleSearchPositionHelper(logger, mockGoogleClient.Object);

            var result = googleHelper.GetPosition(keywords, url);

            Assert.AreEqual(0, result.Count);
        }
    }
}
