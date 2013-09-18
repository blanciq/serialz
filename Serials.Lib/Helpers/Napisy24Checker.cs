using Serials.DAL.Models;

namespace Serials.Lib.Helpers
{
    public class Napisy24Checker
    {
        public SubtitlesType Check(string serialName, string episode)
        {
            var url = "http://napisy24.pl/search.php?str=" + serialName.Replace(" ", "+") + "+" + episode;

            var html = RequestHelper.FetchHtmlFromUrlAsString(url);

            if (html.IndexOf("alt=\"Polski\"") != -1)
                return SubtitlesType.Polish;
            if (html.IndexOf("alt=\"Angielski\"") != -1)
                return SubtitlesType.English;
            return SubtitlesType.None;
        }
    }
}
