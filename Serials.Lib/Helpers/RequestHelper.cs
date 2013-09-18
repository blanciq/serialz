using System.IO;
using System.Net;
using System.Xml.Linq;
using Sgml;

namespace Serials.Lib.Helpers
{
    public static class RequestHelper
    {
        public static XDocument FetchXmlFromUrlAsXDocument(string url)
        {
            var webRequest = WebRequest.Create(url);
            var response = webRequest.GetResponse();
            using (var reader = response.GetResponseStream())
            {
                return XDocument.Load(reader);
            }
        }

        public static XDocument FetchHtmlFromUrlAsXDocument(string url)
        {
            var webRequest = WebRequest.Create(url);
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                var sgml = new SgmlReader();
                sgml.DocType = "HTML";
                sgml.CaseFolding = CaseFolding.ToLower;
                sgml.InputStream = reader;
                return new XDocument(XDocument.Load(sgml));
            }
        }

        public static string FetchResponseString(string url)
        {
            var webRequest = WebRequest.Create(url);
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}