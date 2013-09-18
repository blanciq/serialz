using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Serials.DAL.Models;
using Serials.Lib.Extensions;

namespace Serials.Lib.Helpers
{
    public class WikiParser
    {
        private readonly XDocument _html;
        private string _title;

        public WikiParser(XDocument html)
        {
            _html = html;
        }

        public Serial Parse()
        {
            _title = _html.Descendants("h1").WithId(x => x == "firstHeading").Descendants("i").First().Value;
            var seasonNo = 1;
            return new Serial()
                       {
                           Seasons = _html.Descendants("span").WithId(x => x.StartsWith("Season"))
                                .Select(x => ParseSeason(x, seasonNo++)).Where(x => x != null).ToList(),
                           Title = _title
                       };
        }

        private Season ParseSeason(XElement season, int seasonNo)
        {
            var table = season.Parent.ElementsAfterSelf("table").FirstOrDefault();
            if (table == null) 
                throw new Exception("cannot find table for season " + season.Value);
            try
            {
                return new Season(season.Value, table.Descendants("tr").Skip(1).Select(x => ParseEpisode(x, seasonNo)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Episode ParseEpisode(XElement episode, int seasonNo)
        {
            var values = episode.Elements().Select(x => x.Value).ToList();

            var dateString = values[5].Substring(0, values[5].IndexOf('(') > 0 ? values[5].IndexOf('(') - 1 : values[5].Length);

            return new Episode
                       {
                           Name = values[2],
                           SeasonNumber = seasonNo.AsNumberWithLeadingZero(),
                           EpisodeNumber = int.Parse(values[1]).AsNumberWithLeadingZero(),
                           PremiereDate = GetPremiereDate(dateString),
                           SerialName = _title
                       };
        }

        private static DateTime GetPremiereDate(string dateString)
        {
            try
            {
                dateString = dateString.Replace((char) 160, ' '); // fix for &nbsp; spaces
                return !string.IsNullOrWhiteSpace(dateString)
                           ? DateTime.ParseExact(dateString, "MMMM d, yyyy", new CultureInfo("en-US"))
                           : DateTime.MinValue;
            }
            catch (Exception ex)
            {
                throw new Exception("cannot parse date time " + dateString);
            }
        }
    }
}