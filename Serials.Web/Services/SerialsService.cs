using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Serials.Web.Services
{
    public class SerialsService
    {
        public static string GetWikiUrl(string title)
        {
            var webRequest =
                WebRequest.Create(
                    String.Format("http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=site:en.wikipedia.org%20episodes%{0}", title));

            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                dynamic jsonResponse = JToken.Parse(reader.ReadToEnd());
                return jsonResponse.responseData.results[0].url;
            }
        }

        public static string GetRageTvUrl(string title)
        {
            var webRequest =
                WebRequest.Create("http://services.tvrage.com/feeds/search.php?show=" + title);

            var webResponse = webRequest.GetResponse();
            using (var reader = new StreamReader(webResponse.GetResponseStream()))
            {
                var doc = XDocument.Load(reader);
                var show = doc.Root.Descendants("show").FirstOrDefault();
                if (show == null)
                {
                    throw new Exception("cannot find series with name " + title);
                }
                return "http://services.tvrage.com/feeds/full_show_info.php?sid=" + show.Element("showid").Value;
            }
        }
    }
}
