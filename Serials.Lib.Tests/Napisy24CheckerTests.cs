using NUnit.Framework;
using Serials.DAL.Models;
using Serials.Lib.Helpers;

namespace Serials.Lib.Tests
{
    [TestFixture]
    [Ignore("Only for manual testing")]
    public class Napisy24CheckerTests
    {
        [Test]
        public void ThereShouldBeSubtitlesForHIMYMS01E01()
        {
            var checker = new Napisy24Checker();
            
            var result = checker.Check("How I Met Your Mother", "s01e01");

            Assert.AreEqual(SubtitlesType.Polish, result);
        }

        [Test]
        public void ThereShouldNotBeSubtitlesForHIMYMS08E021()
        {
            var checker = new Napisy24Checker();

            var result = checker.Check("How I Met Your Mother", "s08e02");

            Assert.AreEqual(SubtitlesType.English, result);
        }
    }
}
