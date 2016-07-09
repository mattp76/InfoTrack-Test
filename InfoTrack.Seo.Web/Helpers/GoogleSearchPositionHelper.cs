using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using InfoTrack.Seo.Web.Interfaces;
using log4net;
using System.Net.Http;
using System.Threading.Tasks;
using InfoTrack.Seo.Web.Clients;

namespace InfoTrack.Seo.Web.Helpers
{

    public class GoogleSearchPositionHelper : IGoogleSearchPositionHelper
    {
        /// <summary>
        /// GoogleClient and Logger are injected, using AutoFac.
        /// </summary>
        protected readonly ILog _logger;
        protected readonly IGoogleClient _client;

        public GoogleSearchPositionHelper(ILog logger, IGoogleClient client)
        {
            _logger = logger;
            _client = client;
        }


        /// <summary>
        /// Get the position of the url within a google search.
        /// </summary>
        /// <returns>A list of integers representing the position(s) of the URL.</returns>
        public List<int> GetPosition(string keywords, string url)
        {
            int counter = -1;
            List<int> positions = new List<int>();

            try
            {
                var html = _client.getGoogleResponse(keywords);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var nodes = doc.DocumentNode.SelectNodes("//h3/a[@href]");
    
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        counter++;

                        foreach (var attribute in node.Attributes)
                        {
                            if (attribute.Name == "href" && attribute.Value.Contains(url) && counter < 100)
                            {
                                positions.Add(counter);
                            }
                        }
                    }

                    return positions;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return positions;
        }
    }
}