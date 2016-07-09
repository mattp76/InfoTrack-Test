using System;
using System.Collections.Generic;
using log4net;
using System.Linq;
using System.Net.Http;
using System.Web;
using InfoTrack.Seo.Web.Interfaces;

namespace InfoTrack.Seo.Web.Clients
{
    public class GoogleClient : IGoogleClient
    {
        /// <summary>
        /// Logger is injected in via AutoFac with the Google Client is constructed
        /// </summary>
        protected readonly ILog _logger;

        public GoogleClient(ILog logger)
        {
            _logger = logger;
        }

        public string getGoogleResponse(string keywords)
        {
            /// <summary>
            /// HttpClient is used to handle the request to Google.
            /// </summary>
            /// <returns>Either a string of HTML or, if no response, an empty string</returns>
            string raw = "http://www.google.com/search?num=100&q={0}&btnG=Search";
            string search = string.Format(raw, HttpUtility.UrlEncode(keywords));

            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(search).Result;
                    HttpContent content = response.Content;
                    return content.ReadAsStringAsync().Result;
                }
                catch (HttpRequestException ex)
                {
                    // Handle exception.
                    _logger.Error(ex.ToString());
                }

                return string.Empty;
            }
        }
    }
}