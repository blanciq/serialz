using System.IO;
using System.Net;
using NUnit.Framework;

namespace Serials.Lib.Tests
{
    class RequestTest
    {
        [Test]
        public void CanAccessSite()
        {
            var webRequest = WebRequest.Create("http://en.wikipedia.org/wiki/List_of_How_I_Met_Your_Mother_episodes");

            var response = webRequest.GetResponse();
        }

        [Test]
        public void CanReadFromResponse()
        {
            var webRequest = WebRequest.Create("http://en.wikipedia.org/wiki/List_of_How_I_Met_Your_Mother_episodes");
            var response = webRequest.GetResponse();

            string result;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

    }
}
