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
    public class HomeController : Controller
    {

        private readonly IGoogleSearchPositionHelper _googleSearchPositionHelper;
     
        public HomeController(IGoogleSearchPositionHelper googleSearchPositionHelper)
        {
            _googleSearchPositionHelper = googleSearchPositionHelper;
        }

        public ActionResult Index()
        {
            ISearchViewModel searchViewModel = new SearchViewModel();
            return View("Index", searchViewModel);
        }


        [HttpPost]
        public ActionResult Index(SearchViewModel searchViewModel)
        {
            List<int> positions;
            
            if (ModelState.IsValid)
            {
                positions = _googleSearchPositionHelper.GetPosition(searchViewModel.Url, searchViewModel.Keywords);
                
                if (positions != null)
                {
                    ViewData["Positions"] = positions;
                }
            }

            return View("Index", searchViewModel);
        }

    }
}
