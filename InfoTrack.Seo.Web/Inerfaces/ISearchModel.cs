using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfoTrack.Seo.Web.Interfaces
{
    public interface ISearchModel
    {
        string Keywords { get; set; }

        string Url { get; set; }

        List<int> Positions { get; set; }
    }
}