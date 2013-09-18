using System;
using System.Collections.Generic;
using System.Linq;

namespace Serials.DAL.Models
{
    public class Serial
    {
        public List<Season> Seasons { get; set; }
        public string Title { get; set; }

        public List<Episode> GetAllEpisodes(bool currentSeason = false)
        {
            return Seasons
                .Where(x => x.Episodes.Any(y => y.PremiereDate > DateTime.Now))
                .SelectMany(x => x.Episodes).Where(x => x.PremiereDate != DateTime.MinValue).ToList();
        }
    }
}