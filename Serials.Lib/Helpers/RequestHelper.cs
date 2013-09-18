using System.IO;
using System.Net;
using System.Xml.Linq;
using Sgml;

namespace Serials.Lib.Helpers
{
    public static class RequestHelper
    {
        public static XDocument FetchHtmlFromUrlAsXDocument(string url)
        {
            var stringHtml = FetchHtmlFromUrlAsString(url);
            using (var reader = new StringReader(stringHtml))
            {
                var sgml = new SgmlReader();
                sgml.DocType = "HTML";
                sgml.CaseFolding = CaseFolding.ToLower;
                sgml.InputStream = reader;
                return new XDocument(XDocument.Load(sgml));
            }
        }

        public static string FetchHtmlFromUrlAsString(string url)
        {
            var webRequest = WebRequest.Create(url);
            var response = webRequest.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}