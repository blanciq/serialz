using System.Diagnostics;
using NUnit.Framework;
using Serials.Lib.Helpers;
using Serials.Web.Services;

namespace Serials.Web.IntegrationTests
{
    class IntegrationTests
    {
        [Test]
        public void DisplayAllEpisodesOfHowIMetYourMother()
        {
            var doc = RequestHelper.FetchHtmlFromUrlAsXDocument("http://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes");
            var parser = new WikiParser(doc);
            var model = parser.Parse();
            Trace.WriteLine((string) model.Title);
            foreach (var episode in model.GetAllEpisodes())
            {
                Trace.WriteLine(episode.Name + " " + episode.PremiereDate.ToShortDateString());
            }
        }

        [Test]
        public void GetFindWikiSiteWithSeries()
        {
            var url = SerialsService.GetWikiUrl("Dexter");

            Assert.AreEqual("http://en.wikipedia.org/wiki/List_of_Dexter_episodes", url);
        }
    }
}
