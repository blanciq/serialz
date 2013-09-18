using System;
using System.Diagnostics;
using System.Globalization;
using NUnit.Framework;

namespace Serials.Lib.Tests
{
    class DateTimeParsingTests
    {
        private string _dateString = "September 22, 2008".Replace((char)160, ' ');

        [Test]
        public void NAME()
        {
            var date = new DateTime(2008, 9, 22);

            var result = date.ToString("MMMM d, yyyy", new CultureInfo("en-US"));

            Assert.AreEqual(_dateString, result);
        }

        [Test]
        public void CanParse()
        {
            var result = DateTime.ParseExact(_dateString, "MMMM d, yyyy", new CultureInfo("en-US"));

            Assert.AreEqual(new DateTime(2008, 9, 22), result);
        }
    }
}
