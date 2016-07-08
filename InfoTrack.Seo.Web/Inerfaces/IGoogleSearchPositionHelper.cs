using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace InfoTrack.Seo.Web.Interfaces
{

    public interface IGoogleSearchPositionHelper
    {
        List<int> GetPosition(string url, string searchTerm);
        List<int> FindPosition(string html, string url);
    }
}