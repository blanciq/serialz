using System.Collections.Generic;
using Serials.DAL.Models;

namespace Serials.Web.Models
{
    public class HomePageViewModel
    {
        public IList<SerialSummary> Serials { get; set; }
    }
}