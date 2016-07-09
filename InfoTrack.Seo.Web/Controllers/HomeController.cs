using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfoTrack.Seo.Web.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using InfoTrack.Seo.Web.Interfaces;

namespace InfoTrack.Seo.Web.Controllers
{
    /// <summary>
    ///  
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// googleSearchPositionHelper is injected, using AutoFac. See ioc/AppRegistration for dependancies being registered.
        /// </summary>
        private readonly IGoogleSearchPositionHelper _googleSearchPositionHelper;
     
        public HomeController(IGoogleSearchPositionHelper googleSearchPositionHelper)
        {
            _googleSearchPositionHelper = googleSearchPositionHelper;
        }

        /// <summary>
        /// Displays an index page.
        /// </summary>
        /// <returns>An index page with searchViewModel</returns>
        public ActionResult Index()
        {
            ISearchViewModel searchViewModel = new SearchViewModel();
            return View("Index", searchViewModel);
        }

        /// <summary>
        /// Called on POST.Values are bound to the searchViewModel, 
        /// which are then passed into othe _googleSearchPositionHelper
        /// </summary>
        /// <returns>An index page with searchViewModel. The resulting list of positions is added to the searchViewModel.</returns>
        [HttpPost]
        public ActionResult Index(SearchViewModel searchViewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", searchViewModel);

            searchViewModel.Positions = _googleSearchPositionHelper.GetPosition(searchViewModel.Keywords, searchViewModel.Url);
            
            return View("Index", searchViewModel);
        }
    }
}
