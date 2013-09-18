using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Serials.DAL.Models;

namespace Serials.Lib.Helpers
{
    public class TvRageParser
    {
        private readonly XDocument _html;

        public TvRageParser(XDocument html)
        {
            _html = html;
        }

        public Serial Parse()
        {
            var title = _html.Root.Element("name").Value;
            return new Serial
                {
                    Title = title,
                    Seasons = new List<Season>(_html.Root.Descendants("Season").Select(x => new Season(title,
                        x.Descendants("episode").Select(episode => new Episode
                            {
                                EpisodeNumber = episode.Element("seasonnum").Value,
                                Name = episode.Element("title").Value,
                                PremiereDate = DateTime.Parse(episode.Element("airdate").Value, CultureInfo.InvariantCulture),
                                SeasonNumber = x.Attribute("no").Value,
                                SerialName = title
                            }))))
                };
        }
    }
}
