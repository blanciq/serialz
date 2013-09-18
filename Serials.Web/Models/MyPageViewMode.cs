using System.Collections.Generic;
using Serials.DAL.Models;

namespace Serials.Web.Models
{
    public class MyPageViewMode
    {
        public List<string> Serials { get; set; } 
        public List<Episode> UpcommingEpisodes { get; set; }
        public List<Episode> RecentEpisodes { get; set; }

        public MyPageViewMode()
        {
            UpcommingEpisodes = new List<Episode>();
            RecentEpisodes = new List<Episode>();
            Serials = new List<string>();
        }
    }
}