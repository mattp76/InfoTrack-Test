using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InfoTrack.Seo.Web.Interfaces;

namespace InfoTrack.Seo.Web.Models
{
    public class SearchModel : ISearchModel
    {
        [Required]
        public string Keywords { get; set; }

        [Required]
        public string Url { get; set; }

        public List<int> Positions { get; set; }
    }
}