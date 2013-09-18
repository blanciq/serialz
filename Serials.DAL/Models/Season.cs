using System;
using System.Collections.Generic;

namespace Serials.DAL.Models
{
    public class Season
    {
        public string Title { get; set; }
        public List<Episode> Episodes { get; set; }

        public Season()
        {
        }

        public Season(string title, IEnumerable<Episode> episodes)
        {
            Episodes = new List<Episode>(episodes);
            Title = title;
        }
    }
}