using System;
using System.IO;
using System.Net;
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
    }
}
