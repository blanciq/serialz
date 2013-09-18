using System;

namespace Serials.DAL.Models
{
    public class Episode
    {
        public string Name { get; set; }
        public string SeasonNumber { get; set; }
        public string EpisodeNumber { get; set; }

        public string Number
        {
            get { return string.Format("s{0}e{1}", SeasonNumber, EpisodeNumber); }
        }

        public DateTime PremiereDate { get; set; }
        public string SerialName { get; set; }
        public SubtitlesType Subtitles { get; set; }
    }
}