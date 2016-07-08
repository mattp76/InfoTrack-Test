using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using InfoTrack.Seo.Web.Interfaces;

namespace InfoTrack.Seo.Web.Helpers
{

    public class GoogleSearchPositionHelper : IGoogleSearchPositionHelper
    {

        public log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(GoogleSearchPositionHelper));

        public List<int> GetPosition(string url, string searchTerm)
        {
            try
            {
                string raw = "http://www.google.com/search?num=100&q={0}&btnG=Search";
                string search = string.Format(raw, HttpUtility.UrlEncode(searchTerm));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(search);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                    {
                        string html = reader.ReadToEnd();
                        return FindPosition(html, url);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return null;
        }


        public List<int> FindPosition(string html, string url)
        {
            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var nodes = doc.DocumentNode.SelectNodes("//h3/a[@href]");
                int counter = -1;
                List<int> positions = new List<int>();

                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        counter++;

                        foreach (var attribute in node.Attributes)
                        {
                            if (attribute.Value.Contains(url) && counter < 100)
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

            return null;
        }
    }
}