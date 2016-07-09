using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace InfoTrack.Seo.Web.Interfaces
{

    public interface IGoogleClient
    {
        String getGoogleResponse(string keywords);
    }
}