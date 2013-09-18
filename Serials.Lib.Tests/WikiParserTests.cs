using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using Serials.Lib.Helpers;
using Sgml;

namespace Serials.Lib.Tests
{
    public class WikiParserTests
    {
        private WikiParser _sut;

        [SetUp]
        public void Before()
        {
            _sut = new WikiParser(Html);
        }


        [Test]
        public void CanParseResponseAsXML()
        {
            using(var inputReader = new StreamReader(new FileStream("fixture.txt", FileMode.Open)))
            {
                var reader = new SgmlReader();
                reader.InputStream = inputReader;
                reader.CaseFolding = CaseFolding.ToLower;
                reader.DocType = "HTML";

                var document = new XDocument(XDocument.Load((XmlReader) reader));

                Assert.IsNotNull(document.ToString());
            }
        }

        [Test]
        public void CanFindSeasonParagraphs()
        {
            var xElements = Html.Descendants("span").Where(x => x.Attribute("id") != null && x.Attribute("id").Value.StartsWith("Season"));

            Assert.AreEqual(8, xElements.Count());
        }


        [Test]
        public void Parse_SerialHasProperName()
        {
            var model = _sut.Parse();

            Assert.AreEqual("How I Met Your Mother", model.Title);
        }

        [Test]
        public void Parse_EightSeasons_ShouldReturnEightSesason()
        {
            var model = _sut.Parse();

            Assert.AreEqual(8, model.Seasons.Count);
        }

        [Test]
        public void Parse_ProperNameOfFirstSeason()
        {
            var model = _sut.Parse();

            Assert.AreEqual("Season 1: 2005–2006", model.Seasons.First().Title);
        }

        [Test]
        public void Parse_FirstSeasonGot22Episodes()
        {
            var model = _sut.Parse();

            Assert.AreEqual(22, model.Seasons.First().Episodes.Count());
        }

        [Test]
        public void Parse_FirstEpisodeOfFirstSeasonName()
        {
            var model = _sut.Parse();

            Assert.AreEqual("\"Pilot\"", model.Seasons.First().Episodes.First().Name);

        }
        [Test]
        public void Parse_FirstEpisodeOfFirstSeasonFullNumber()
        {
            var model = _sut.Parse();

            Assert.AreEqual("s01e01", model.Seasons.First().Episodes.First().Number);
        }

        [Test]
        public void Parse_FirstEpisodeDate()
        {
            var model = _sut.Parse();

            Assert.AreEqual(new DateTime(2005, 9, 19), model.Seasons.First().Episodes.First().PremiereDate);
        }

        [Test]
        public void Parse_FirstEpisodeHasSerialName()
        {
            var model = _sut.Parse();

            Assert.AreEqual("How I Met Your Mother", model.Seasons.First().Episodes.First().SerialName);
        }

        [Test]
        public void Parse_FirstEpisodeHasNumber()
        {
            var model = _sut.Parse();

            Assert.AreEqual("s01e01", model.Seasons.First().Episodes.First().Number);
        }

        private static XDocument Html
        {
            get
            {
                using (var inputReader = new StreamReader(new FileStream("fixture.txt", FileMode.Open)))
                {
                    var reader = new SgmlReader();
                    reader.InputStream = inputReader;
                    reader.DocType = "HTML";

                    var document = new XDocument(XDocument.Load((XmlReader) reader));

                    return document;
                }
            }
        }
    }
}
