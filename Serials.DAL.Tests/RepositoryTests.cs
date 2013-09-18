using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Serials.DAL.Models;
using Serials.DAL.Repositories;
using ServiceStack.Text;

namespace Serials.DAL.Tests
{
    [TestFixture]
    public class RepositoryTests : RedisTestsBase
    {
        private SerialRepository _sut;

        [SetUp]
        public void Initialize()
        {
            _sut = new SerialRepository();
        }

        [Test]
        public void SavingSerialWithSeasons()
        {
            var serial = GetSerialWithTwoEpisodes();

            _sut.Save(serial);

            Assert.AreEqual(2, client.As<Serial>().GetAll().First().Seasons.Count);
        }
        
        [Test]
        public void SavingSerialWithSeasonsAndEpisodes()
        {
            var serial = GetSerialWithTwoEpisodes();
            serial.Seasons.First().Episodes = new List<Episode>
                {
                    new Episode
                        {
                            Subtitles = SubtitlesType.Polish
                        }
                };

            _sut.Save(serial);

            Assert.AreEqual(SubtitlesType.Polish,
                client.As<Serial>().GetAll().First().Seasons.First().Episodes.First().Subtitles);
        }

        private static Serial GetSerialWithTwoEpisodes()
        {
            return new Serial
                {
                    Title = "Title",
                    Seasons = new List<Season>
                        {
                            new Season {Title = "Season1"},
                            new Season {Title = "Season2"}
                        }
                };
        }
    }
}
